using HttpMultipartParser;
using System.Text;

namespace RestServer.Http
{
    public class HttpRequest
    {
        public string Url { get; set; }

        //Url = path+querystring
        public string Method { get; set; }
        public string Path { get; set; } 
        public string HttpVersion { get; set; }

        //headers
        public HttpHeaders Headers { get; set; }

        //body bytes
        public byte[] BodyBytes { get; set; }

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

        public Route Route { get; set; }

        public QueryParameter Query { get; set; }

        public MultipartFormDataParser MultipartFormData { get; set; }

        public HttpRequest()
        {
            this.Headers = new HttpHeaders();
            //Multiparts = new List<Multipart>();
        }

        public override string ToString()
        {
            string content = "";
            if (BodyBytes != null) {
                content = Encoding.UTF8.GetString(BodyBytes);
            }
            return string.Format("{0} {1} {2}\r\n{3}\r\n\r\n{4}", Method,
                Url,
                HttpVersion,
                Headers.ToString(),
                content);
        }
    }
}
