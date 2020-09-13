using System.Collections.Generic;
using System.Text;

namespace RestServer.Http
{
    public enum HttpStatusCode
    {

        Continue = 100,

        Ok = 200,
        Created = 201,
        Accepted = 202,
        MovedPermanently = 301,
        Found = 302,
        NotModified = 304,
        BadRequest = 400,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        InternalServerError = 500
    }

    public class HttpResponse
    {
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public byte[] Data { get; set; }

        public HttpHeaders Headers { get; set; }

        public string ContentAsUTF8
        {
            set
            {
                this.setContent(value, encoding: Encoding.UTF8);
            }
        }
        public void setContent(string content, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            Data = encoding.GetBytes(content);
        }

        public HttpResponse()
        {
            this.Headers = new HttpHeaders();
        }

        public override string ToString()
        {
            return string.Format("HTTP status {0} {1}", this.StatusCode, this.StatusDescription);
        }
    }
}
