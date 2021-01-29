using RestServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestServer.Http
{
    public class HttpHeaders
    {
        public static string ACCEPT = "Accept";

        public static string ACCEPT_CHARSET = "Accept-Charset";

        public static string ACCEPT_ENCODING = "Accept-Encoding";

        public static string ACCEPT_LANGUAGE = "Accept-Language";

        public static string ACCEPT_RANGES = "Accept-Ranges";

        public static string ACCESS_CONTROL_ALLOW_CREDENTIALS = "Access-Control-Allow-Credentials";

        public static string ACCESS_CONTROL_ALLOW_HEADERS = "Access-Control-Allow-Headers";


        public static string ACCESS_CONTROL_ALLOW_METHODS = "Access-Control-Allow-Methods";

        public static string ACCESS_CONTROL_ALLOW_ORIGIN = "Access-Control-Allow-Origin";
        /**
         * The CORS {@code Access-Control-Expose-Headers} response header field name.
         * @see <a href="http://www.w3.org/TR/cors/">CORS W3C recommendation</a>
         */
        public static string ACCESS_CONTROL_EXPOSE_HEADERS = "Access-Control-Expose-Headers";
        /**
         * The CORS {@code Access-Control-Max-Age} response header field name.
         * @see <a href="http://www.w3.org/TR/cors/">CORS W3C recommendation</a>
         */
        public static string ACCESS_CONTROL_MAX_AGE = "Access-Control-Max-Age";
        /**
         * The CORS {@code Access-Control-Request-Headers} request header field name.
         * @see <a href="http://www.w3.org/TR/cors/">CORS W3C recommendation</a>
         */
        public static string ACCESS_CONTROL_REQUEST_HEADERS = "Access-Control-Request-Headers";
        /**
         * The CORS {@code Access-Control-Request-Method} request header field name.
         * @see <a href="http://www.w3.org/TR/cors/">CORS W3C recommendation</a>
         */
        public static string ACCESS_CONTROL_REQUEST_METHOD = "Access-Control-Request-Method";
        /**
         * The HTTP {@code Age} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7234#section-5.1">Section 5.1 of RFC 7234</a>
         */
        public static string AGE = "Age";
        /**
         * The HTTP {@code Allow} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-7.4.1">Section 7.4.1 of RFC 7231</a>
         */
        public static string ALLOW = "Allow";
        /**
         * The HTTP {@code Authorization} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7235#section-4.2">Section 4.2 of RFC 7235</a>
         */
        public static string AUTHORIZATION = "Authorization";
        /**
         * The HTTP {@code Cache-Control} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7234#section-5.2">Section 5.2 of RFC 7234</a>
         */
        public static string CACHE_CONTROL = "Cache-Control";
        /**
         * The HTTP {@code Connection} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-6.1">Section 6.1 of RFC 7230</a>
         */
        public static string CONNECTION = "Connection";
        /**
         * The HTTP {@code Content-Encoding} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-3.1.2.2">Section 3.1.2.2 of RFC 7231</a>
         */
        public static string CONTENT_ENCODING = "Content-Encoding";
        /**
         * The HTTP {@code Content-Disposition} header field name
         * @see <a href="http://tools.ietf.org/html/rfc6266">RFC 6266</a>
         */
        public static string CONTENT_DISPOSITION = "Content-Disposition";
        /**
         * The HTTP {@code Content-Language} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-3.1.3.2">Section 3.1.3.2 of RFC 7231</a>
         */
        public static string CONTENT_LANGUAGE = "Content-Language";
        /**
         * The HTTP {@code Content-Length} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-3.3.2">Section 3.3.2 of RFC 7230</a>
         */
        public static string CONTENT_LENGTH = "Content-Length";
        /**
         * The HTTP {@code Content-Location} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-3.1.4.2">Section 3.1.4.2 of RFC 7231</a>
         */
        public static string CONTENT_LOCATION = "Content-Location";
        /**
         * The HTTP {@code Content-Range} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7233#section-4.2">Section 4.2 of RFC 7233</a>
         */
        public static string CONTENT_RANGE = "Content-Range";
        /**
         * The HTTP {@code Content-Type} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-3.1.1.5">Section 3.1.1.5 of RFC 7231</a>
         */
        public static string CONTENT_TYPE = "Content-Type";
        /**
         * The HTTP {@code Cookie} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc2109#section-4.3.4">Section 4.3.4 of RFC 2109</a>
         */
        public static string COOKIE = "Cookie";
        /**
         * The HTTP {@code Date} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-7.1.1.2">Section 7.1.1.2 of RFC 7231</a>
         */
        public static string DATE = "Date";
        /**
         * The HTTP {@code ETag} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-2.3">Section 2.3 of RFC 7232</a>
         */
        public static string ETAG = "ETag";
        /**
         * The HTTP {@code Expect} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-5.1.1">Section 5.1.1 of RFC 7231</a>
         */
        public static string EXPECT = "Expect";
        /**
         * The HTTP {@code Expires} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7234#section-5.3">Section 5.3 of RFC 7234</a>
         */
        public static string EXPIRES = "Expires";
        /**
         * The HTTP {@code From} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-5.5.1">Section 5.5.1 of RFC 7231</a>
         */
        public static string FROM = "From";
        /**
         * The HTTP {@code Host} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-5.4">Section 5.4 of RFC 7230</a>
         */
        public static string HOST = "Host";
        /**
         * The HTTP {@code If-Match} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-3.1">Section 3.1 of RFC 7232</a>
         */
        public static string IF_MATCH = "If-Match";
        /**
         * The HTTP {@code If-Modified-Since} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-3.3">Section 3.3 of RFC 7232</a>
         */
        public static string IF_MODIFIED_SINCE = "If-Modified-Since";
        /**
         * The HTTP {@code If-None-Match} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-3.2">Section 3.2 of RFC 7232</a>
         */
        public static string IF_NONE_MATCH = "If-None-Match";
        /**
         * The HTTP {@code If-Range} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7233#section-3.2">Section 3.2 of RFC 7233</a>
         */
        public static string IF_RANGE = "If-Range";
        /**
         * The HTTP {@code If-Unmodified-Since} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-3.4">Section 3.4 of RFC 7232</a>
         */
        public static string IF_UNMODIFIED_SINCE = "If-Unmodified-Since";
        /**
         * The HTTP {@code Last-Modified} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-2.2">Section 2.2 of RFC 7232</a>
         */
        public static string LAST_MODIFIED = "Last-Modified";
        /**
         * The HTTP {@code Link} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc5988">RFC 5988</a>
         */
        public static string LINK = "Link";
        /**
         * The HTTP {@code Location} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-7.1.2">Section 7.1.2 of RFC 7231</a>
         */
        public static string LOCATION = "Location";
        /**
         * The HTTP {@code Max-Forwards} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-5.1.2">Section 5.1.2 of RFC 7231</a>
         */
        public static string MAX_FORWARDS = "Max-Forwards";
        /**
         * The HTTP {@code Origin} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc6454">RFC 6454</a>
         */
        public static string ORIGIN = "Origin";
        
        public static string RANGE = "Range";
        /**
         * The HTTP {@code Referer} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-5.5.2">Section 5.5.2 of RFC 7231</a>
         */
        public static string REFERER = "Referer";

        /**
         * The HTTP {@code Server} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-7.4.2">Section 7.4.2 of RFC 7231</a>
         */
        public static string SERVER = "Server";
        /**
         * The HTTP {@code Set-Cookie} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc2109#section-4.2.2">Section 4.2.2 of RFC 2109</a>
         */
        public static string SET_COOKIE = "Set-Cookie";
        
        /**
         * The HTTP {@code User-Agent} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-5.5.3">Section 5.5.3 of RFC 7231</a>
         */
        public static string USER_AGENT = "User-Agent";
       

        private Dictionary<string, string> Headers { get; set; }

        public string this[string name]
        {
            get
            {
                return Headers[name];
            }
        }

        public HttpHeaders()
        {
            Headers = new Dictionary<string, string>();
        }

        public bool Contains(string key)
        {
            return Headers.ContainsKey(key);
        }
        public void Add(string key, string value)
        {
            Headers.Add(key, value);
        }

        public override string ToString()
        {
            return string.Join("\r\n", this.Headers.Select(x => string.Format("{0}: {1}", x.Key, x.Value)));
        }

        public void SetAllowedOrigin(string value)
        {
            Add(ACCESS_CONTROL_ALLOW_ORIGIN, value);
        }
        public void SetContentType(string value) {
            Add(CONTENT_TYPE, value);
        }
        public void SetContentTypeWithDefaultCharset(string value)
        {
            Add(CONTENT_TYPE, value + HttpConfig.CONTENT_TYPE_CHARSET_PREFIX + HttpConfig.DefaultCharsetEncoding);
        }
        public void SetContentLength(string value) {
            Add(CONTENT_LENGTH, value.ToString());
        }
        public void SetContentEncoding(string value) {
            Add(CONTENT_ENCODING, value);
        }
        public void SetAllowCredentials(bool b) {
            Add(ACCESS_CONTROL_ALLOW_CREDENTIALS, b.ToString());
        }
        public void SetAllowOrigin(string s)
        {
            Add(ACCESS_CONTROL_ALLOW_ORIGIN, s);
        }
        public void SetServer(string name) {
            Add(SERVER, name);
        }
        public void SetDate(string v) {
            Add(DATE, v);
        }
    }
}
