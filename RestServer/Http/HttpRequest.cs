using HttpMultipartParser;
using System.Text;

namespace RestServer.Http
{
    public class HttpRequest
    {
        //Url = path+querystring
        public string Method { get; set; }
        public string Url { get; set; }
        public string HttpVersion { get; set; }

        public string Path { get; set; }

        //headers
        public HttpHeaders Headers { get; set; }

        //body bytes
        private byte[] _bodyBytes;
        public byte[] BodyBytes { 
            get { return _bodyBytes; }
            set {
                _bodyBytes = value;

                if(Method == HttpMethod.GET.ToString())
                {
                    return;
                }
                string tp = Headers.GetContentType();
                if (tp != null)
                {
                    if (tp.StartsWith("application/x-www-form-urlencoded"))
                    {
                        Query.Parse(Encoding.UTF8.GetString(_bodyBytes));
                    }

                }
            }
        }

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
                Query.Parse(_queryString);
            }
        }

        public QueryParameter Query { get; set; }

        public MultipartFormDataParser MultipartFormData { get; set; }

        public HttpRequest()
        {
            this.Headers = new HttpHeaders();
            Query = new QueryParameter();
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
