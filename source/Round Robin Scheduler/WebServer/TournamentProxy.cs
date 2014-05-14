using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using SomeTechie.RoundRobinScheduler.Properties;

namespace SomeTechie.RoundRobinScheduler.WebServer
{
    public enum TournamentProxyState { Starting, Error, Started, Paused, Stopping, Stopped };
    class TournamentProxy
    {
        protected const string proxyClearStaticFilesPath = "/clearstaticfiles.php";
        protected const string proxyAcceptStaticFilesPath = "/acceptstaticfile.php";
        protected const string proxyGetRequestsPath = "/getrequests.php";
        protected const string proxyReplyPath = "/reply.php";
        private bool getRequestsThreadShouldPause;
        private object getRequestsThreadLock = new object();
        protected System.Threading.Thread getRequestsThread;
        protected static JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        protected List<System.Threading.Thread> processRequestThreads = new List<System.Threading.Thread>();

        public event EventHandler StateChanged;

        protected Uri _proxyUrl;
        public Uri ProxyUrl
        {
            get
            {
                return _proxyUrl;
            }
        }

        protected string _sharedKeyUrlEncoded;
        protected string _sharedKey;
        public string SharedKey
        {
            get
            {
                return _sharedKey;
            }
        }

        protected TournamentWebServer _webServer;

        private TournamentProxyState _state = TournamentProxyState.Stopped;
        public TournamentProxyState State
        {
            get
            {
                return _state;
            }
            protected set
            {
                _state = value;
                if (StateChanged != null) TriggerEvent(StateChanged, this, EventArgs.Empty);
            }
        }

        public TournamentProxy(Uri proxyUrl,string sharedKey, TournamentWebServer WebServer)
        {
            System.Windows.Forms.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            if (proxyUrl == null) throw new ArgumentException(String.Format(Resources.ProxyInvalidUrl, ""), "proxyUrl");
            _proxyUrl = proxyUrl;
            _sharedKey = sharedKey;
            _sharedKeyUrlEncoded = HttpUtility.UrlEncode(sharedKey);
            _webServer = WebServer;
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            Stop();
        }

        public void Pause()
        {
            if (State == TournamentProxyState.Started)
            {
                getRequestsThreadShouldPause = true;
               
                State = TournamentProxyState.Paused;
            }
        }

        public void Resume()
        {
            if (State == TournamentProxyState.Paused)
            {
                getRequestsThreadShouldPause = false;
                lock (getRequestsThreadLock)
                {
                    System.Threading.Monitor.PulseAll(getRequestsThreadLock);
                }

                State = TournamentProxyState.Started;
            }
        }

        public void Start()
        {
            if (State != TournamentProxyState.Stopped) return;
            try
            {
                State = TournamentProxyState.Starting;

                ClearProxyStaticFiles();
                UploadProxyStaticFiles();
                startGettingRequests();

                State = TournamentProxyState.Started;
            }
            catch
            {
                State = TournamentProxyState.Stopped;
                throw;
            }
        }

        protected void startGettingRequests()
        {
            System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(getRequests);
            getRequestsThread = new System.Threading.Thread(threadStart);
            getRequestsThread.Start();
        }

        protected void getRequests()
        {
            bool continueGettingRequests = true;
            while (continueGettingRequests)
            {
                if (getRequestsThreadShouldPause)
                {
                    lock (getRequestsThreadLock)
                    {
                        System.Threading.Monitor.Wait(getRequestsThreadLock);
                    }
                }
                try
                {
                    string result = new WebClient().DownloadString(ProxyUrl + proxyGetRequestsPath + "?sharedkey=" + _sharedKeyUrlEncoded);
                    object[] requests = (object[])jsonSerializer.DeserializeObject(result);
                    if (requests != null)
                    {
                        foreach (object request in requests)
                        {
                            if (request == null) continue;
                            try
                            {
                                System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(() => handleRequest((Dictionary<string, Object>)request));
                                System.Threading.Thread thread = new System.Threading.Thread(threadStart);
                                thread.Start();
                                
                            }
                            catch (Exception ex)
                            {
                                Program.logException(ex, String.Format(Resources.ProxyProcessRequestsFailed, request.ToString()));
                            }
                        }
                    }
                    State = TournamentProxyState.Started;
                }
                catch (System.Threading.ThreadAbortException) { }
                catch (Exception ex)
                {
                    State = TournamentProxyState.Error;
                    Program.logException(ex, getRequestsThreadShouldPause ? Resources.ProxyGetRequestsFailedPossibleThreadPause : Resources.ProxyGetRequestsFailed);
                }
            }
        }

        protected const string idKey = "id";
        protected const string dataKey = "d";
        protected const string pathKey = "path";
        protected const string cookiesKey = "cookies";
        protected void handleRequest(Dictionary<string, object> request)
        {
            processRequestThreads.Add(System.Threading.Thread.CurrentThread);
            if (!request.ContainsKey(idKey) || !request.ContainsKey(pathKey)) return;
            int id = (int)request[idKey];

            object responceObj = null;
            try
            {
                WebServerRequest webServerRequest = new WebServerRequest();
                webServerRequest.Path = (string)request[pathKey];

                if (request.ContainsKey(dataKey))
                {
                    if (request[dataKey] is string) webServerRequest.Data = (string)request[dataKey];
                    else if (request[dataKey] != null) webServerRequest.Data = request[dataKey];
                }
                if (request.ContainsKey(cookiesKey) && request[cookiesKey] is Dictionary<string, object>)
                {
                    System.Collections.Specialized.NameValueCollection cookies = new System.Collections.Specialized.NameValueCollection();
                    foreach (KeyValuePair<string, object> cookie in (Dictionary<string, object>)request[cookiesKey])
                    {
                        if (!(cookie.Value is string)) continue;
                        cookies.Add(cookie.Key, (string)cookie.Value);
                    }
                    webServerRequest.Cookies = cookies;
                }


                WebServerResponse response = _webServer.generateResponse(webServerRequest);
                List<SerializableKeyValuePair<string, string>> headers = new List<SerializableKeyValuePair<string, string>>();
                for (int i = 0; i < response.Headers.AllKeys.Length; i++)
                {
                    string key = response.Headers.Keys[i];
                    foreach (string value in response.Headers.GetValues(i))
                    {
                        headers.Add(new SerializableKeyValuePair<string, string>(key, value));
                    }
                }
                responceObj = new { id = id, status = response.StatusCode, body = response.BodyString, headers = headers };
            }
            catch (HttpException ex)
            {
                responceObj = new { id = id, status = ex.GetHttpCode().ToString() + " " + ex.Message };
            }
            catch
            {
                responceObj = new { id = id, status = "500 Internal Server Error" };
            }
            reply(responceObj);
            processRequestThreads.Remove(System.Threading.Thread.CurrentThread);
        }

        protected void reply(object data)
        {
            try
            {
                if (data == null) return;
                object replyData = (data is object[]) ? data : new object[] { data };
                string replyStr = jsonSerializer.Serialize(replyData);
                string result = new WebClient().UploadString(ProxyUrl.ToString() + proxyReplyPath + "?sharedkey=" + _sharedKeyUrlEncoded, replyStr);
            }
            catch (Exception ex)
            {
                State = TournamentProxyState.Error;
                Program.logException(ex, Resources.ProxyReplyFailed);
            }
        }

        protected void UploadProxyStaticFiles()
        {
            UploadProxyStaticFiles(_webServer.HtdocsPath, _webServer.HtdocsPath.FullName);
        }

        protected void UploadProxyStaticFiles(DirectoryInfo directory, string rootPath)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                if (file.Name[0] == '.')
                {
                    continue;
                }
                UploadProxyStaticFile(file, file.FullName.Substring(rootPath.Length));
            }
            foreach (DirectoryInfo subdirectory in directory.GetDirectories())
            {
                UploadProxyStaticFiles(subdirectory, rootPath);
            }
        }

        protected void UploadProxyStaticFile(FileInfo file, string destPath)
        {
            try
            {
                // This fixes the "417 Expectation Failed" error
                System.Net.ServicePointManager.Expect100Continue = false;

                string destPathCleaned = destPath.Trim(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }).Replace(Path.DirectorySeparatorChar, '/').Replace(Path.AltDirectorySeparatorChar, '/');

                byte[] result = new WebClient().UploadFile(_proxyUrl + proxyAcceptStaticFilesPath + "?sharedkey=" + _sharedKeyUrlEncoded + "&uploadpath=" + System.Web.HttpUtility.UrlEncode(destPathCleaned), "POST", file.FullName);
                string resultStr = System.Text.Encoding.ASCII.GetString(result);
                if (resultStr != "true")
                {
                    Program.logException(resultStr, String.Format(Resources.ProxyUploadStaticFileFailed, destPath));
                    throw new Exception("UploadProxyStaticFile; Unexpected result: " + resultStr);
                }
            }
            catch (Exception ex)
            {
                Program.logException(ex, String.Format(Resources.ProxyUploadStaticFileFailed, destPath));
                throw;
            }
        }

        protected void ClearProxyStaticFiles()
        {
            try
            {
                // This fixes the "417 Expectation Failed" error
                System.Net.ServicePointManager.Expect100Continue = false;

                string result = new WebClient().DownloadString(_proxyUrl + proxyClearStaticFilesPath + "?sharedkey=" + _sharedKeyUrlEncoded);
                if (result != "true")
                {
                    Program.logException(result, Resources.ProxyClearStaticFilesFailed);
                    throw new Exception("ClearProxyStaticFiles; Unexpected result: " + result);
                }
            }
            catch (Exception ex)
            {
                Program.logException(ex, Resources.ProxyClearStaticFilesFailed);
                throw;
            }
        }

        public void Stop()
        {
            if (State == TournamentProxyState.Stopped) return;
            try
            {
                State = TournamentProxyState.Stopping;

                //Stop the get requests thread
                getRequestsThreadShouldPause = true;
                if (getRequestsThread != null) getRequestsThread.Abort();
                getRequestsThread = null;

                //End all process request threads
                foreach (System.Threading.Thread requestThread in processRequestThreads)
                {
                    requestThread.Abort();
                }
                processRequestThreads.Clear();

                //Clear static files
                ClearProxyStaticFiles();
            }
            catch
            {
                State = TournamentProxyState.Stopped;
                throw;
            }
            State = TournamentProxyState.Stopped;
        }

        protected void TriggerEvent(Delegate e, params object[] args)
        {
            foreach (Delegate del in e.GetInvocationList())
            {
                try
                {
                    System.ComponentModel.ISynchronizeInvoke syncer = del.Target as System.ComponentModel.ISynchronizeInvoke;
                    if (syncer == null)
                    {
                        del.DynamicInvoke(args);
                    }
                    else
                    {
                        syncer.BeginInvoke(del, args);
                    }
                }
                catch { }
            }
        }
    }
}