using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace SomeTechie.RoundRobinScheduler.WebServer
{
    public class CustomHttpResponse
    {
        protected Encoding _encoding = new UTF8Encoding();
        public Encoding Encoding
        {
            get
            {
                return _encoding;
            }
            set
            {
                _encoding = value;
            }
        }

        protected NameValueCollection _headers = new NameValueCollection();
        public NameValueCollection Headers
        {
            get
            {
                return _headers;
            }
        }

        protected string _httpVersion = "HTTP/1.1";
        public string HttpVersion
        {
            get
            {
                return _httpVersion;
            }
            set
            {
                _httpVersion = value;
            }
        }

        protected string _statusCode = "200 OK";
        public string StatusCode
        {
            get
            {
                return _statusCode;
            }
            set
            {
                _statusCode = value;
            }
        }

        protected byte[] _bodyData;
        public byte[] BodyData
        {
            get
            {
                return _bodyData;
            }
            set
            {
                _bodyData = value;
            }
        }

        public string BodyString
        {
            get
            {
                return Encoding.GetString(BodyData);
            }
            set
            {
                BodyData = Encoding.GetBytes(value);
            }
        }

        public CustomHttpResponse()
        {
        }

        public CustomHttpResponse(System.Web.HttpException httpException)
        {
            _statusCode = httpException.GetHttpCode().ToString() + " " + httpException.Message;
        }

        public CustomHttpResponse(WebServerResponse webServerResponse)
        {
            _statusCode = webServerResponse.StatusCode;
            _headers = webServerResponse.Headers;
            if (webServerResponse.BodyData != null) _bodyData = webServerResponse.BodyData;
            else BodyString = webServerResponse.BodyString;
        }

        public void ToStream(System.IO.Stream stream, bool addContentLengthHeader = true)
        {
            //Create the writer
            System.IO.TextWriter responseWriter = new System.IO.StreamWriter(stream, _encoding);

            //Write http version
            responseWriter.WriteLine(string.Format("{0} {1}", HttpVersion, StatusCode));

            //Add Content-Length header if needed
            if (addContentLengthHeader && BodyData != null && Headers["Content-Length"] == null)
            {
                Headers.Add("Content-Length", BodyData.Length.ToString());
            }

            //Write response headers
            for (int i = 0; i < Headers.AllKeys.Length; i++)
            {
                string key = Headers.Keys[i];
                foreach (string value in Headers.GetValues(i))
                {
                    responseWriter.WriteLine("{0}: {1}", key, value);
                }
            }


            //Write response body
            responseWriter.WriteLine();
            responseWriter.Flush();
            if (BodyData != null) stream.Write(BodyData, 0, BodyData.Length);
            responseWriter.Close();

        }
    }
}