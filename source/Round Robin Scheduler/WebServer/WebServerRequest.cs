using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace SomeTechie.RoundRobinScheduler.WebServer
{
    public class WebServerRequest
    {
        protected NameValueCollection _cookies = new NameValueCollection();
        public NameValueCollection Cookies
        {
            get
            {
                return _cookies;
            }
            set
            {
                _cookies = value;
            }
        }

        protected string _path;
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
        
        protected string _body;
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        protected object _data;
        public object Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }


        public WebServerRequest()
        {
        }
    }
}
