using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using SomeTechie.RoundRobinScheduler.WebData;
using SomeTechie.RoundRobinScheduler.Properties;
using SomeTechie.RoundRobinScheduleGenerator;

namespace SomeTechie.RoundRobinScheduler.WebServer
{
    public enum TournamentWebServerState { Started, Stopped, Paused };
    public delegate object JSONCallHandler(string data, Dictionary<string, object> sessionData);
    public class TournamentWebServer
    {
        public event EventHandler StateChanged;

        protected static JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

        protected List<string> _indexPages = new List<string>(new string[] { "index.html", "index.htm" });
        public List<string> IndexPages
        {
            get
            {
                return _indexPages;
            }
            set
            {
                _indexPages = value;
            }
        }

        protected IPAddress _iPAddress = IPAddress.Any;
        public IPAddress ListenIP
        {
            get
            {
                return listenerEndPoint.Address;
            }
            set
            {
                if (_iPAddress != value)
                {
                    _iPAddress = value;
                    if (listener != null) Restart();
                }
            }
        }

        public int ListenPort
        {
            get
            {
                if (listenerEndPoint != null)
                {
                    return listenerEndPoint.Port;
                }
                else
                {
                    return -1;
                }
            }
        }

        public TournamentWebServerState State
        {
            get
            {
                if (listener == null) return TournamentWebServerState.Stopped;
                else if (listenThreadShouldPause == true) return TournamentWebServerState.Paused;
                else return TournamentWebServerState.Started;
            }
        }

        protected Controller Controller = Controller.GetController();

        private Dictionary<string, Dictionary<string, object>> sessionsData = new Dictionary<string, Dictionary<string, object>>();

        static Random r = new Random();
        private bool listenThreadShouldPause;
        private object listenThreadLock = new object();
        protected System.Threading.Thread listenThread;
        protected IPEndPoint listenerEndPoint;
        protected TcpListener listener;

        protected DirectoryInfo _htdocsPath = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "htdocs"));
        public DirectoryInfo HtdocsPath
        {
            get
            {
                return _htdocsPath;
            }
            set
            {
                _htdocsPath = value;
            }
        }

        protected Dictionary<string, JSONCallHandler> _JSONCallHandlers = new Dictionary<string, JSONCallHandler>();
        public Dictionary<string, JSONCallHandler> JSONCallHandlers
        {
            get
            {
                return _JSONCallHandlers;
            }
            set
            {
                _JSONCallHandlers = value;
            }
        }

        public TournamentWebServer()
        {
            System.Windows.Forms.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

            JSONCallHandlers.Add("accesscode", JSONGetAccessCode);
            JSONCallHandlers.Add("strings", JSONGetStrings);
            JSONCallHandlers.Add("currentscorekeeper", JSONGetCurrentScoreKeeper);
            JSONCallHandlers.Add("scorekeeperschedule", JSONGetScoreKeeperSchedule);
            JSONCallHandlers.Add("game", JSONGetGame);
            JSONCallHandlers.Add("currentgame", JSONGetCurrentGame);
            JSONCallHandlers.Add("tournamentstatus", JSONGetTournamentStatus);
            JSONCallHandlers.Add("team", JSONGetTeam);
            JSONCallHandlers.Add("setgameresult", JSONSetGameResult);
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.Stop();
        }

        public IPAddress[] GetAccessibleIPs()
        {
            NetworkInterface[] networkInterfaces = GetNetworkInterfaces();
            List<NetworkInterface> upInterfaces = new List<NetworkInterface>();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                bool isUp = networkInterface.OperationalStatus == OperationalStatus.Up;
                if (isUp) upInterfaces.Add(networkInterface);
            }

            List<NetworkInterface> externalInterfaces = new List<NetworkInterface>();
            foreach (NetworkInterface networkInterface in upInterfaces)
            {
                IPInterfaceProperties iPInterfaceProperties = networkInterface.GetIPProperties();
                bool isAvailable = false;
                foreach (GatewayIPAddressInformation iPAddressInfo in iPInterfaceProperties.GatewayAddresses)
                {
                    try
                    {
                        if (new Ping().Send(iPAddressInfo.Address).Status == IPStatus.Success)
                        {
                            isAvailable = true;
                            break;
                        }
                    }
                    catch { }
                    try
                    {
                        System.Net.Sockets.TcpClient clnt = new System.Net.Sockets.TcpClient(iPAddressInfo.Address.ToString(), 80);
                        clnt.Close();
                        isAvailable = true;
                        break;
                    }
                    catch { }
                }
                if (isAvailable) externalInterfaces.Add(networkInterface);
            }

            IPAddress[] iPAddresses;
            iPAddresses = getIPsForInterfaces(externalInterfaces.ToArray(), AddressFamily.InterNetwork);
            if (iPAddresses.Length < 1) iPAddresses = getIPsForInterfaces(upInterfaces.ToArray(), AddressFamily.InterNetwork);
            if (iPAddresses.Length < 1) iPAddresses = new IPAddress[] { IPAddress.Loopback };

            return iPAddresses.ToArray();
        }
        protected IPAddress[] getIPsForInterfaces(NetworkInterface[] networkInterfaces, AddressFamily addressFamily)
        {
            return getIPsForInterfaces(networkInterfaces, new AddressFamily[] { addressFamily });
        }
        protected IPAddress[] getIPsForInterfaces(NetworkInterface[] networkInterfaces, AddressFamily[] addressFamilies)
        {
            List<IPAddress> iPAddresses = new List<IPAddress>();
            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                iPAddresses.AddRange(getIPsForInterface(networkInterface, addressFamilies));
            }

            return iPAddresses.ToArray();
        }
        protected IPAddress[] getIPsForInterface(NetworkInterface networkInterface, AddressFamily addressFamily)
        {
            return getIPsForInterface(networkInterface, new AddressFamily[] { addressFamily });
        }
        protected IPAddress[] getIPsForInterface(NetworkInterface networkInterface, AddressFamily[] addressFamilies = null)
        {
            List<IPAddress> iPAddresses = new List<IPAddress>();
            IPInterfaceProperties iPInterfaceProperties = networkInterface.GetIPProperties();
            foreach (IPAddressInformation iPAddressInfo in iPInterfaceProperties.UnicastAddresses)
            {
                IPAddress iPAddress = iPAddressInfo.Address;
                if (!IPAddress.IsLoopback(iPAddress) && (addressFamilies == null || addressFamilies.Contains(iPAddress.AddressFamily)))
                {
                    iPAddresses.Add(iPAddress);
                }
            }

            return iPAddresses.ToArray();
        }
        protected NetworkInterface[] GetNetworkInterfaces()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
        }

        private int GetPossiblePort()
        {
            IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpListeners = iPGlobalProperties.GetActiveTcpListeners();
            List<int> usedPorts = new List<int>();
            foreach (IPEndPoint tcpListener in tcpListeners)
            {
                usedPorts.Add(tcpListener.Port);
            }

            int listenPort = -1;
            if (!usedPorts.Contains(80))
            {
                listenPort = 80;
            }
            else
            {
                int minPort = Math.Max(49152, IPEndPoint.MinPort);
                int maxPort = IPEndPoint.MaxPort;
                for (int port = minPort; port <= maxPort; port++)
                {
                    if (!usedPorts.Contains(port))
                    {
                        listenPort = port;
                        break;
                    }
                }
            }
            return listenPort;
        }

        public void Restart()
        {
            Stop();
            Start();
        }

        public void Start()
        {
            Program.writeToLog(Resources.WebServerStartedLogMessage);
            bool started = false;
            for (var i = 0; i < 5; i++)
            {
                try
                {
                    if (listener == null)
                    {
                        startListening();
                    }
                    listener.Start();
                    started = true;
                }
                catch (Exception ex)
                {
                    Program.logException(ex, Resources.WebServerStartFailed);
                    if (listener != null)
                    {
                        try
                        {
                            listener.Stop();
                            listener.Server.Close();
                        }
                        catch { }
                        listener = null;
                    }
                }
            }
            if (!started) throw new Exception(Resources.WebServerStartFailed5Times);
            listenThreadShouldPause = false;
            lock (listenThreadLock)
            {
                System.Threading.Monitor.Pulse(listenThreadLock);
            }
            if (StateChanged != null) StateChanged(this, new EventArgs());
        }

        public void Pause()
        {
            if (State == TournamentWebServerState.Started)
            {
                listenThreadShouldPause = true;
                if (listener != null) listener.Stop();

                Program.writeToLog(Resources.WebServerPausedLogMessage);
                if (StateChanged != null) StateChanged(this, new EventArgs());
            }
        }

        public void Resume()
        {
            if (State == TournamentWebServerState.Paused)
            {
                listenThreadShouldPause = false;
                lock (listenThreadLock)
                {
                    System.Threading.Monitor.PulseAll(listenThreadLock);
                }
                if (listener != null) listener.Start();

                Program.writeToLog(Resources.WebServerResumedLogMessage);
                if (StateChanged != null) StateChanged(this, new EventArgs());
            }
        }

        public void Stop()
        {
            Program.writeToLog(Resources.WebServerStoppedLogMessage);
            listenThreadShouldPause = true;
            if (listener != null)
            {
                listener.Stop();
                listener.Server.Close();
            }
            if (listenThread != null) listenThread.Abort();
            listener = null;
            listenThread = null;
            listenerEndPoint = null;

            if (StateChanged != null) StateChanged(this, new EventArgs());
        }

        protected void startListening()
        {
            if (listener != null) return;

            int listenPort = GetPossiblePort();

            if (listenPort < 0)
            {
                listenerEndPoint = null;
            }
            else
            {
                listenerEndPoint = new IPEndPoint(_iPAddress, listenPort);
                listener = new TcpListener(listenerEndPoint);
            }

            System.Threading.ThreadStart threadStart = new System.Threading.ThreadStart(listen);
            listenThread = new System.Threading.Thread(threadStart);
            listenThread.Start();
        }

        protected void listen()
        {
            bool continueListening = true;
            try
            {
                listener.Start();
            }
            catch
            {
                try
                {
                    listener.Start();
                }
                catch
                {
                    Stop();
                    return;
                }
            }
            while (continueListening)
            {
                if (listenThreadShouldPause)
                {
                    lock (listenThreadLock)
                    {
                        System.Threading.Monitor.Wait(listenThreadLock);
                    }
                }
                try
                {
                    TcpClient client = listener.AcceptTcpClient();

                    System.Threading.ThreadStart ts = new System.Threading.ThreadStart(() => handleClient(client));
                    System.Threading.Thread thread = new System.Threading.Thread(ts);
                    thread.Start();
                }
                catch (System.Threading.ThreadAbortException) { }
                catch (Exception ex)
                {
                    Program.logException(ex, listenThreadShouldPause ? Resources.WebServerListenFailedPossibleThreadPause : Resources.WebServerListenFailed);
                }
            }
        }

        protected const string dataKey = "d";
        protected void handleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            try
            {
                byte[] data = new byte[client.ReceiveBufferSize];
                int numBytesRead = stream.Read(data, 0, client.ReceiveBufferSize);
                string requestString = new UTF8Encoding().GetString(data, 0, numBytesRead);

                CustomHttpRequest httpRequest = CustomHttpRequest.Parse(requestString);
                WebServerRequest webServerRequest = new WebServerRequest();
                webServerRequest.Cookies = httpRequest.Cookies;
                webServerRequest.Body = httpRequest.Body;
                webServerRequest.Path = httpRequest.RequestTarget.LocalPath;

                //Parse additional data
                Dictionary<string, List<string>> queryParts = ParseQuery(httpRequest.RequestTarget.Query);
                object additionalData = null;
                if (httpRequest.Method == "POST")
                {
                    if (httpRequest.PostData.ContainsKey(dataKey))
                        additionalData = httpRequest.PostData[dataKey];
                    else additionalData = httpRequest.PostData;
                }
                else
                {
                    if (queryParts.ContainsKey(dataKey) && queryParts[dataKey].Count > 0)
                        additionalData = queryParts[dataKey].First();
                }
                webServerRequest.Data = additionalData;

                CustomHttpResponse response = new CustomHttpResponse(generateResponse(webServerRequest));
                response.ToStream(stream);
            }
            catch (System.Web.HttpException ex)
            {
                new CustomHttpResponse(ex).ToStream(stream);
            }
            catch
            {
                if (client.Connected)
                {
                    try
                    {
                        CustomHttpResponse response = new CustomHttpResponse();
                        response.StatusCode = "500 Internal Server Error";
                        response.ToStream(stream);
                    }
                    catch { }
                }
            }

            client.Close();
        }

        public WebServerResponse generateResponse(WebServerRequest request)
        {
            System.Collections.Specialized.NameValueCollection extraHeaders = new System.Collections.Specialized.NameValueCollection();

            //Session handling
            Dictionary<string, object> sessionData;
            if (request.Cookies["SESSID"] != null && sessionsData.ContainsKey(request.Cookies["SESSID"]))
            {
                sessionData = sessionsData[request.Cookies["SESSID"]];
            }
            else
            {
                string sessionId = Guid.NewGuid().ToString("N");
                extraHeaders.Add("Set-Cookie", String.Format("{0}={1}; Path=/", "SESSID", sessionId));
                sessionData = new Dictionary<string, object>();
                sessionsData[sessionId] = sessionData;
            }
          
            string path = request.Path.Trim(new char[] { ' ', '/' });
            WebServerResponse response;
            if (path == "test")
            {
                response = new WebServerResponse();
                //Calculate response body
                string responseBody = "";
                responseBody += "<html><head>\r\n";
                responseBody += "\r\n</head><body>\r\n";
                responseBody += "<h1>Successfully Connected</h1>";
                responseBody += "\r\n</body></html>";
                response.BodyString = responseBody;
            }
            else if (path == "json" || path.StartsWith("json/"))
                response = ServeJson(request, sessionData);
            else
            {
                string filePath = Path.Combine(HtdocsPath.FullName, path.Replace('/', '\\'));
                response = ServeFile(filePath);
            }
            if (extraHeaders.Count > 0) response.Headers.Add(extraHeaders);

            return response;
        }

        protected Dictionary<string, List<string>> ParseQuery(string queryString)
        {
            Dictionary<string, List<string>> queryParts = new Dictionary<string, List<string>>();

            if (!string.IsNullOrEmpty(queryString))
            {
                string query = queryString.Trim().TrimStart(new char[] { '?' });
                string[] rawParts = query.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string rawPart in rawParts)
                {
                    string part = rawPart.Trim();
                    int equalsIndex = part.IndexOf('=');
                    string key = null;
                    string value = null;
                    if (equalsIndex < 0)
                    {
                        key = part;
                    }
                    else
                    {
                        key = part.Substring(0, equalsIndex);
                        if (part.Length > equalsIndex + 1) value = part.Substring(equalsIndex + 1);
                        else value = string.Empty;
                    }

                    if (queryParts.ContainsKey(key))
                    {
                        if (value != null) queryParts[key].Add(value);
                    }
                    else
                    {
                        List<string> values = new List<string>();
                        values.Add(value);
                        queryParts.Add(key, values);
                    }
                }
            }

            return queryParts;
        }

        #region JSON
        protected WebServerResponse ServeJson(WebServerRequest request, Dictionary<string, object> sessionData)
        {
            string contentType = "application/json";
            WebServerResponse response = new WebServerResponse();

            string action = request.Path.Substring("/json".Length).ToLower().Trim(new char[] { '/' });

            if (action.Length > 0 && action != "compound")
            {
                if (JSONCallHandlers.ContainsKey(action))
                {
                    string requestData = null;
                    if (request.Data is string) requestData = (string)request.Data;
                    response.BodyString = jsonSerializer.Serialize(JSONCallHandlers[action](requestData, sessionData));
                }
                else throw new HttpException(404, "Not Found");
            }
            else
            {
                Dictionary<string, string> postData;
                if (request.Data is Dictionary<string, string>)
                    postData = (Dictionary<string, string>)request.Data;
                else if (request.Data is Dictionary<string, object>)
                {
                    try
                    {
                        postData = ((Dictionary<string, object>)request.Data).ToDictionary(k => k.Key, k => !(k.Value is string) ? "" : (string)k.Value);
                    }
                    catch
                    {
                        throw new HttpException(400, "Bad Request");
                    }
                }
                else throw new HttpException(400, "Bad Request");

                Dictionary<string, object> returnData = new Dictionary<string, object>();
                foreach (KeyValuePair<string, string> pair in postData)
                {
                    string key = pair.Key.ToLower();
                    try
                    {
                        if (JSONCallHandlers.ContainsKey(key))
                        {
                            if (!(pair.Value is string))
                            {
                                returnData.Add(key, new { Error = true, ErrorCode = 400, ErrorMessage = "Bad Request" });
                                continue;
                            }
                            returnData.Add(key, JSONCallHandlers[key](pair.Value, sessionData));
                        }
                        else returnData.Add(key, new { Error = true, ErrorCode = 404, ErrorMessage = "Not Found" });
                    }
                    catch (HttpException ex)
                    {
                        returnData.Add(key, new { Error = true, ErrorCode = ex.ErrorCode, ErrorMessage = ex.Message });
                    }
                    catch
                    {
                        returnData.Add(key, new { Error = true, ErrorCode = 500, ErrorMessage = "Internal Server Error" });
                    }
                }
                response.BodyString = jsonSerializer.Serialize(returnData);
            }

            //Set the content type
            response.Headers.Add("Content-Type", contentType);

            return response;
        }

        protected object JSONGetAccessCode(string data, Dictionary<string, object> sessionData)
        {
            return getAccessCode(sessionData);
        }

        protected object JSONGetStrings(string data, Dictionary<string, object> sessionData)
        {
            Properties.Settings settings = new Properties.Settings();
            object strings = new
            {
                TournamentName = Controller.Tournament.Name,
                AccessCodeMessageLine1 = settings.accessCodeMessageMakeBold ? settings.AccessCodeMessageLine1.Replace("{0}", "<b>{0}</b>") : settings.AccessCodeMessageLine1,
                AccessCodeMessageLine2 = settings.accessCodeMessageMakeBold ? settings.AccessCodeMessageLine2.Replace("{0}", "<b>{0}</b>") : settings.AccessCodeMessageLine2
            };

            return strings;
        }

        protected object JSONGetCurrentScoreKeeper(string data, Dictionary<string, object> sessionData)
        {
            ScoreKeeperWebData scoreKeeperData = null;
            if (Controller.Tournament != null)
            {
                if (Controller.ScoreKeepers != null)
                {
                    string accessCode = getAccessCode(sessionData);
                    foreach (ScoreKeeper scoreKeeper in Controller.ScoreKeepers)
                    {
                        if (scoreKeeper.AssociatedAccessCode == accessCode)
                        {
                            scoreKeeperData = new ScoreKeeperWebData(scoreKeeper);
                            break;
                        }
                    }
                }
            }

            return scoreKeeperData;
        }

        protected object JSONGetScoreKeeperSchedule(string data, Dictionary<string, object> sessionData)
        {
            Dictionary<string, object> schedule = new Dictionary<string, object>();

            string accessCode = getAccessCode(sessionData);

            CourtRound currentCourtRound = getCurrentCourtRound(accessCode);
            int? currentCourtRoundNum = null;
            if (currentCourtRound != null) currentCourtRoundNum = currentCourtRound.RoundNumber;

            List<int> startableCourtRoundNums = new List<int>();

            int? activeCourtRoundNum = null;
            CourtRound activeCourtRound = Controller.Tournament.ActiveCourtRound;
            if (activeCourtRound != null) activeCourtRoundNum = activeCourtRound.RoundNumber;

            if (Controller.Tournament != null)
            {
                foreach (CourtRound courtRound in Controller.Tournament.CourtRounds)
                {
                    string key = courtRound.RoundNumber.ToString();
                    object value;
                    Game courtRoundGame = getGameInCourtRoundForAccessCode(courtRound, accessCode);
                    if (courtRoundGame != null)
                    {
                        value = new GameWebData(courtRoundGame);

                        //This game can be manually started
                        if (!courtRoundGame.IsCompleted) startableCourtRoundNums.Add(courtRound.RoundNumber);
                    }
                    else value = new { NotAssigned = true };

                    schedule.Add(key, value);

                }
            }

            return
                new
                {
                    Schedule = schedule,
                    CurrentCourtRoundNum = currentCourtRoundNum,
                    ActiveCourtRoundNum = activeCourtRoundNum,
                    StartableCourtRoundNums = startableCourtRoundNums
                };
        }

        protected object JSONGetGame(string data, Dictionary<string, object> sessionData)
        {
            Game game = null;

            int id;
            if (int.TryParse(data, out id)) game = getGameFromId(id);
            else throw new HttpException(400, "Bad Request");

            GameWebData gameData = null;
            if (game != null) gameData = new GameWebData(game);

            return gameData;
        }

        protected object JSONGetCurrentGame(string data, Dictionary<string, object> sessionData)
        {
            Game game = null;
            bool gameCompleted = false;
            bool notAssigned = false;
            int? currentRound = null;
            if (Controller.Tournament != null)
            {
                CourtRound activeCourtRound = Controller.Tournament.ActiveCourtRound;
                if (activeCourtRound != null)
                {
                    Game activeCourtRoundGame = getGameInCourtRoundForAccessCode(activeCourtRound, getAccessCode(sessionData));
                    if (activeCourtRoundGame != null && !activeCourtRoundGame.IsCompleted) game = activeCourtRoundGame;
                    else
                    {
                        notAssigned = activeCourtRoundGame == null;
                        if (activeCourtRoundGame != null) gameCompleted = activeCourtRoundGame.IsCompleted;
                        currentRound = activeCourtRound.RoundNumber;
                    }
                }
            }


            object gameData = null;
            if (game != null) gameData = new GameWebData(game);
            else gameData = new { NotAssigned = notAssigned, GameCompleted = gameCompleted, CourtRoundNum = currentRound };

            return gameData;
        }

        protected object JSONGetTournamentStatus(string data, Dictionary<string, object> sessionData)
        {
            TournamentStatusWebData tournamentStatusWebData = null;
            if (Controller.Tournament != null) tournamentStatusWebData = new TournamentStatusWebData(Controller.Tournament);

            return tournamentStatusWebData;
        }

        protected object JSONGetTeam(string data, Dictionary<string, object> sessionData)
        {
            Team team = null;
            if (Controller.Tournament != null)
            {
                string id = data;
                RoundRobinTeamData rrTeamData = Controller.Tournament.getTeamDataById(id);
                if (rrTeamData != null)
                {
                    team = rrTeamData.Team;
                }
            }
            TeamWebData teamData = null;
            if (team != null) teamData = new TeamWebData(team);

            return teamData;
        }

        protected object JSONSetGameResult(string data, Dictionary<string, object> sessionData)
        {
            Dictionary<string, object> resultData;
            if (data == null)
            {
                throw new HttpException(400, "Bad Request");
            }
            try
            {
                resultData = (Dictionary<string, object>)jsonSerializer.DeserializeObject(data);
            }
            catch
            {
                throw new HttpException(400, "Bad Request");
            }
            if (resultData == null || !resultData.ContainsKey("GameId") || !(resultData["GameId"] is int)) throw new HttpException(400, "Bad Request");
            int id = (int)resultData["GameId"];

            if (Controller.Tournament != null && Controller.Tournament.GamesById != null)
            {
                Game game = null;
                if (Controller.Tournament.GamesById.ContainsKey(id))
                {
                    game = Controller.Tournament.GamesById[id];
                }

                if (game != null)
                {
                    if (game.ScoreKeeper == null || game.ScoreKeeper.AssociatedAccessCode != getAccessCode(sessionData))
                        throw new HttpException(403, "Forbidden");

                    bool shouldTriggerGameResultChanged = false;
                    try
                    {
                        if (game.IsConfirmed)
                        {
                            game.IsConfirmed = false;
                            shouldTriggerGameResultChanged = true;
                        }
                        if (resultData.ContainsKey("TeamGameResults"))
                        {
                            #region Team game results
                            object teamGameResults = resultData["TeamGameResults"];
                            if (teamGameResults != null)
                            {
                                if (teamGameResults is Dictionary<string, object>)
                                {
                                    foreach (KeyValuePair<string, object> pair in (Dictionary<string, object>)teamGameResults)
                                    {
                                        if (!(pair.Value is Dictionary<string, object>)) throw new HttpException(400, "Bad Request");

                                        Dictionary<string, object> teamGameResultData = (Dictionary<string, object>)pair.Value;
                                        string teamId = pair.Key;
                                        if (!game.TeamGameResults.ContainsKey(teamId)) throw new HttpException(404, "Not Found");

                                        TeamGameResult teamGameResult = game.TeamGameResults[teamId];
                                        if (teamGameResult == null) throw new HttpException(404, "Not Found");


                                        if (teamGameResultData.ContainsKey("NumPoints"))
                                        {
                                            if (teamGameResultData["NumPoints"] != null)
                                            {
                                                if (teamGameResultData["NumPoints"] is int)
                                                {
                                                    int numPoints = (int)teamGameResultData["NumPoints"];
                                                    if (teamGameResult.NumPoints != numPoints)
                                                    {
                                                        teamGameResult.NumPoints = numPoints;
                                                        shouldTriggerGameResultChanged = true;
                                                    }
                                                }
                                                else throw new HttpException(400, "Bad Request");
                                            }
                                        }

                                        if (teamGameResultData.ContainsKey("NumFouls"))
                                        {
                                            if (teamGameResultData["NumFouls"] != null)
                                            {
                                                if (teamGameResultData["NumFouls"] is int)
                                                {
                                                    int numFouls = (int)teamGameResultData["NumFouls"];
                                                    if (teamGameResult.NumFouls != numFouls)
                                                    {
                                                        teamGameResult.NumFouls = numFouls;
                                                        shouldTriggerGameResultChanged = true;
                                                    }
                                                }
                                                else throw new HttpException(400, "Bad Request");
                                            }
                                        }

                                        if (teamGameResultData.ContainsKey("WonGame"))
                                        {
                                            if (teamGameResultData["WonGame"] != null)
                                            {
                                                if (teamGameResultData["WonGame"] is bool)
                                                {
                                                    bool wonGame = (bool)teamGameResultData["WonGame"];
                                                    if (teamGameResult.WonGame != wonGame)
                                                    {
                                                        teamGameResult.WonGame = wonGame;
                                                        shouldTriggerGameResultChanged = true;
                                                    }
                                                }
                                                else throw new HttpException(400, "Bad Request");
                                            }
                                        }
                                    }
                                }
                                else throw new HttpException(400, "Bad Request");
                            }
                            #endregion
                        }

                        if (resultData.ContainsKey("WinningTeam"))
                        {
                            object winningTeam = resultData["WinningTeam"];
                            if (winningTeam != null)
                            {
                                RoundRobinTeamData teamData = Controller.Tournament.getTeamDataById(winningTeam.ToString().Trim());
                                if (teamData != null)
                                {
                                    if (game.WinningTeam != teamData.Team)
                                    {
                                        game.WinningTeam = teamData.Team;
                                        shouldTriggerGameResultChanged = true;
                                    }
                                }
                                else throw new HttpException(404, "Not Found");
                            }
                        }
                    }
                    finally
                    {
                        if (shouldTriggerGameResultChanged) Controller.TriggerGameResultChanged(game);
                    }
                }
                else throw new HttpException(404, "Not Found");
            }
            else throw new HttpException(404, "Not Found");

            return null;
        }
        #endregion

        protected WebServerResponse ServeFile(string filePath)
        {
            WebServerResponse response = new WebServerResponse();
            string contentType = "text/html";
            if (Directory.Exists(filePath))
            {
                foreach (string indexPage in _indexPages)
                {
                    string path = Path.Combine(filePath, indexPage);
                    if (File.Exists(path))
                    {
                        filePath = path;
                        break;
                    }
                }
            }
            if (File.Exists(filePath))
            {
                Encoding fileEncoding;
                bool isText = IsText(out fileEncoding, filePath);
                if (isText)
                {
                    StreamReader streamReader = new StreamReader(filePath);
                    response.BodyString = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                else
                {
                    FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    byte[] data = new byte[fileStream.Length];
                    fileStream.Read(data, 0, (int)fileStream.Length);
                    fileStream.Close();
                    response.BodyData = data;
                }
                contentType = MimeTypeHelper.GetMimeTypeByFilePath(filePath);
            }
            else
            {
                throw new HttpException(404, "Not Found");
            }

            //Set the content type
            response.Headers.Add("Content-Type", contentType);

            return response;
        }

        /// <summary>
        /// Detect if a file is text and detect the encoding.
        /// </summary>
        /// <param name="encoding">
        /// The detected encoding.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="windowSize">
        /// The number of characters to use for testing.
        /// </param>
        /// <returns>
        /// true if the file is text.
        /// </returns>
        protected static bool IsText(out Encoding encoding, string fileName, int windowSize = 32)
        {
            using (var fileStream = File.OpenRead(fileName))
            {
                var rawData = new byte[windowSize];
                var text = new char[windowSize];
                var isText = true;

                // Read raw bytes
                var rawLength = fileStream.Read(rawData, 0, rawData.Length);
                fileStream.Seek(0, SeekOrigin.Begin);

                // Detect encoding correctly (from Rick Strahl's blog)
                // http://www.west-wind.com/weblog/posts/2007/Nov/28/Detecting-Text-Encoding-for-StreamReader
                if (rawData[0] == 0xef && rawData[1] == 0xbb && rawData[2] == 0xbf)
                {
                    encoding = Encoding.UTF8;
                }
                else if (rawData[0] == 0xfe && rawData[1] == 0xff)
                {
                    encoding = Encoding.Unicode;
                }
                else if (rawData[0] == 0 && rawData[1] == 0 && rawData[2] == 0xfe && rawData[3] == 0xff)
                {
                    encoding = Encoding.UTF32;
                }
                else if (rawData[0] == 0x2b && rawData[1] == 0x2f && rawData[2] == 0x76)
                {
                    encoding = Encoding.UTF7;
                }
                else
                {
                    encoding = Encoding.Default;
                }

                // Read text and detect the encoding
                using (var streamReader = new StreamReader(fileStream))
                {
                    streamReader.Read(text, 0, text.Length);
                }

                using (var memoryStream = new MemoryStream())
                {
                    using (var streamWriter = new StreamWriter(memoryStream, encoding))
                    {
                        // Write the text to a buffer
                        streamWriter.Write(text);
                        streamWriter.Flush();

                        // Get the buffer from the memory stream for comparision
                        var memoryBuffer = memoryStream.GetBuffer();

                        // Compare only bytes read
                        for (var i = 0; i < rawLength && isText; i++)
                        {
                            isText = rawData[i] == memoryBuffer[i];
                        }
                    }
                }

                return isText;
            }
        }

        protected string getAccessCode(Dictionary<string, object> sessionData)
        {
            string accessCode;
            if (sessionData.ContainsKey("accesscode"))
            {
                accessCode = (string)sessionData["accesscode"];
            }
            else
            {
                accessCode = r.Next(10000, 99999).ToString();
                sessionData["accesscode"] = accessCode;
                Controller.ScoreKeeperAccessCodes.Add(accessCode);
                Controller.TriggerScoreKeeperAccessCodesChanged();
            }
            return accessCode;
        }

        protected Game getGameFromId(int id)
        {
            Game game = null;

            if (Controller.Tournament != null)
            {
                if (Controller.Tournament.GamesById.ContainsKey(id))
                {
                    game = Controller.Tournament.GamesById[id];
                }
            }

            return game;
        }
        protected Game getGameInCourtRoundForAccessCode(CourtRound courtRound, string accessCode)
        {
            foreach (Game game in courtRound.Games)
            {
                if (game != null && game.ScoreKeeper != null && game.ScoreKeeper.AssociatedAccessCode == accessCode) return game;
            }
            return null;
        }
        protected CourtRound getCurrentCourtRound(ScoreKeeper scoreKeeper)
        {
            if (scoreKeeper != null) return getCurrentCourtRound(scoreKeeper.AssociatedAccessCode);
            else return null;
        }
        protected CourtRound getCurrentCourtRound(string scoreKeeperAccessCode)
        {
            if (Controller.Tournament != null)
            {
                CourtRound activeCourtRound = Controller.Tournament.ActiveCourtRound;
                if (activeCourtRound != null)
                {
                    Game courtRoundGame = getGameInCourtRoundForAccessCode(activeCourtRound, scoreKeeperAccessCode);
                    if (courtRoundGame != null)
                    {
                        if (!courtRoundGame.IsCompleted) return activeCourtRound;
                        else
                        {
                            if (Controller.Tournament.CourtRounds.Count > activeCourtRound.RoundNumber)
                            {
                                CourtRound nextCourtRound = Controller.Tournament.CourtRounds[activeCourtRound.RoundNumber];
                                return nextCourtRound;
                            }
                            else return null;
                        }
                    }
                    else
                    {
                        if (Controller.Tournament.CourtRounds.Count > activeCourtRound.RoundNumber)
                        {
                            CourtRound nextCourtRound = Controller.Tournament.CourtRounds[activeCourtRound.RoundNumber];
                            Game nextCourtRoundGame = getGameInCourtRoundForAccessCode(nextCourtRound, scoreKeeperAccessCode);
                            if (nextCourtRoundGame != null)
                            {
                                Game gameOnNextCourt = null;
                                if (activeCourtRound.Games.Count >= nextCourtRoundGame.CourtNumber) gameOnNextCourt = activeCourtRound.Games[nextCourtRoundGame.CourtNumber - 1];
                                if (gameOnNextCourt != null)
                                {
                                    if (!gameOnNextCourt.IsCompleted) return activeCourtRound;
                                    else return nextCourtRound;
                                }
                                else return activeCourtRound;
                            }
                            else return activeCourtRound;
                        }
                        else return activeCourtRound;
                    }
                }
            }
            return null;
        }
    }
}