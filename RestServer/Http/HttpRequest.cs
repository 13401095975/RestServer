using HttpMultipartParser;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RestServer.Http
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

        public HttpHeaders Headers { get; set; }

        public QueryParameter Query { get; set; }

        public byte[] BodyBytes { get; set; }

        public MultipartFormDataParser MultipartFormData { get; set; }

        public StreamReader InputStream { 
            get {
                return new StreamReader(new MemoryStream(BodyBytes), Encoding.UTF8);
            } 
        }

        public HttpRequest()
        {
            this.Headers = new HttpHeaders();
            //Multiparts = new List<Multipart>();
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(this.Content))
            {
                if (!this.Headers.Contains("Content-Length"))
                {
                    this.Headers.Add("Content-Length", this.Content.Length.ToString());
                }
            }
            return string.Format("{0} {1} HTTP/1.0\r\n{2}\r\n\r\n{3}", this.Method, this.Url, Headers.ToString(), this.Content);


        }
    }
}
