using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace SomeTechie.RoundRobinScheduler.WebServer
{
    public class WebServerResponse
    {
        protected NameValueCollection _headers = new NameValueCollection();
        public NameValueCollection Headers
        {
            get
            {
                return _headers;
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
                _bodyString = null;
            }
        }

        protected string _bodyString;
        public string BodyString
        {
            get
            {
                return _bodyString;
            }
            set
            {
                _bodyString= value;
                _bodyData=null;
            }
        }

        public WebServerResponse()
        {
        }

        public WebServerResponse(System.Web.HttpException httpException)
        {
            StatusCode = httpException.GetHttpCode().ToString() + " " + httpException.Message;
        }
    }
}