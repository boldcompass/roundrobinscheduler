using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace SomeTechie.RoundRobinScheduler.WebServer
{
    public class CustomHttpRequest
    {
        protected NameValueCollection _headers = new NameValueCollection();
        public NameValueCollection Headers
        {
            get
            {
                return _headers;
            }
        }

        protected Dictionary<string,string> _postData = null;
        public Dictionary<string, string> PostData
        {
            get
            {
                return _postData;
            }
        }

        protected NameValueCollection _cookies = new NameValueCollection();
        public NameValueCollection Cookies
        {
            get
            {
                return _cookies;
            }
        }

        protected string _method;
        public string Method
        {
            get
            {
                return _method;
            }
        }

        protected Uri _requestTarget;
        public Uri RequestTarget
        {
            get
            {
                return _requestTarget;
            }
        }

        protected string _httpVersion;
        public string HttpVersion
        {
            get
            {
                return _httpVersion;
            }
        }

        protected string _body;
        public string Body
        {
            get
            {
                return _body;
            }
        }

        
        protected CustomHttpRequest()
        {
        }

        public static CustomHttpRequest Parse(string str)
        {
            string[] stringLines = str.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            CustomHttpRequest request = new CustomHttpRequest();

            string body = "";

            bool foundStartLine = false;
            bool foundBody = false;
            for (int i = 0; i < stringLines.Length; i++)
            {
                string line = stringLines[i];

                if (!foundBody)
                {
                    if (line.Length < 0 && line.Trim().Length > 0)
                    {
                        throw new System.Web.HttpException(400, "Bad Request");
                    }

                    //If we haven't found the start line
                    if (!foundStartLine && line.Length < 1) continue;
                    //If this is the start line
                    else if (!foundStartLine)
                    {
                        string[] startLineParts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (startLineParts.Length < 2)
                        {
                            throw new System.Web.HttpException(400, "Bad Request");
                        }

                        request._method = startLineParts[0];
                        request._requestTarget = new Uri(new Uri("http://localhost"),startLineParts[1]);
                        if (startLineParts.Length > 2) request._httpVersion = startLineParts[2];

                        foundStartLine = true;
                    }
                    //If this is the separator between the header and the body
                    else if (line.Length < 1)
                    {
                        foundBody = true;
                        continue;
                    }
                    //This is a header field
                    else
                    {
                        int semicolonPosition = line.IndexOf(':');
                        if (semicolonPosition == -1)
                        {
                            throw new System.Web.HttpException(400, "Bad Request");
                        }
                        string key = line.Substring(0, semicolonPosition);
                        if (key.Contains(" "))
                        {
                            throw new System.Web.HttpException(400, "Bad Request");
                        }
                        string value = line.Substring(semicolonPosition + 1).Trim();
                        if (request._headers[key] == null)
                        {
                            request._headers.Add(key, value);
                        }
                        else
                        {
                            request._headers[key] += "," + value;
                        }
                    }
                }
                else if (foundBody)
                {
                    body += "\r\n" + line;
                }
            }
            request._body = body;

            //Parse POST request if needed
            if (request.Method.ToLower() == "post")
            {

                request._postData = new Dictionary<string, string>();
                if (request.Body.Trim().Length > 0)
                {
                    string rawData = request.Body.Trim();
                    string[] rawParts = rawData.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string rawPart in rawParts)
                    {
                        string part = rawPart.Trim();
                        int equalsIndex = part.IndexOf('=');
                        string key = null;
                        string value = null;
                        if (equalsIndex < 0)
                        {
                            continue;
                        }
                        else
                        {
                            key = part.Substring(0, equalsIndex);
                            if (part.Length > equalsIndex + 1)
                            {
                                value = part.Substring(equalsIndex + 1);
                                value = System.Web.HttpUtility.UrlDecode(value);
                            }
                            else value = string.Empty;
                        }

                        if (!request._postData.ContainsKey(key))
                        {
                            request._postData.Add(key, value);
                        }
                    }
                }
            }

            //Parse cookies if needed
            if (request.Headers["Cookie"] != null)
            {
                string[] cookies = request.Headers["Cookie"].Split(new char[] { ';' });
                foreach (string cookie in cookies)
                {
                    string trimmedCookie = cookie.Trim();
                    int equalsPosition = trimmedCookie.IndexOf('=');
                    if (equalsPosition == -1)
                    {
                        continue;
                    }

                    string key = trimmedCookie.Substring(0, equalsPosition).Trim();
                    string value = trimmedCookie.Substring(equalsPosition + 1).Trim();
                    request._cookies.Add(key, value);
                }
            }

            return request;
        }
    }
}
