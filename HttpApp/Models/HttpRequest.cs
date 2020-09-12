using HttpApp;
using System.Collections.Generic;
using System.Linq;

namespace SimpleHttpServer.Models
{
    public class HttpRequest
    {
        //Url = path+querystring
        public string Method { get; set; }
        public string Url { get; set; }
        public string Path { get; set; } 

        private string _queryString;
        public string QueryString
        {
            get
            {
                return _queryString;
            }
            set
            {
                _queryString = value;
                Query = new QueryParameter(_queryString);
            }
        }


        public string Content { get; set; }
        public Route Route { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public QueryParameter Query { get; set; }

        public HttpRequest()
        {
            this.Headers = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(this.Content))
                if (!this.Headers.ContainsKey("Content-Length"))
                    this.Headers.Add("Content-Length", this.Content.Length.ToString());

            return string.Format("{0} {1} HTTP/1.0\r\n{2}\r\n\r\n{3}", this.Method, this.Url, string.Join("\r\n", this.Headers.Select(x => string.Format("{0}: {1}", x.Key, x.Value))), this.Content);
        }
    }
}
