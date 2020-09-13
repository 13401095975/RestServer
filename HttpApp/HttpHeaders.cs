using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp
{
    public class HttpHeaders
    {
        private Dictionary<String, List<String>> headers;

        public HttpHeaders()
        {
            headers = new Dictionary<String, List<String>>();
        }

        public static String ACCEPT = "Accept";

        public static string ACCEPT_CHARSET = "Accept-Charset";

        public static String ACCEPT_ENCODING = "Accept-Encoding";

        public static String ACCEPT_LANGUAGE = "Accept-Language";

        public static String ACCEPT_RANGES = "Accept-Ranges";

        public static String ACCESS_CONTROL_ALLOW_CREDENTIALS = "Access-Control-Allow-Credentials";

        public static String ACCESS_CONTROL_ALLOW_HEADERS = "Access-Control-Allow-Headers";

        public static String ACCESS_CONTROL_ALLOW_METHODS = "Access-Control-Allow-Methods";

        public static String ACCESS_CONTROL_ALLOW_ORIGIN = "Access-Control-Allow-Origin";

        public static String ACCESS_CONTROL_EXPOSE_HEADERS = "Access-Control-Expose-Headers";

        public static String ACCESS_CONTROL_MAX_AGE = "Access-Control-Max-Age";
        /**
         * The CORS {@code Access-Control-Request-Headers} request header field name.
         * @see <a href="http://www.w3.org/TR/cors/">CORS W3C recommendation</a>
         */
        public static String ACCESS_CONTROL_REQUEST_HEADERS = "Access-Control-Request-Headers";
        /**
         * The CORS {@code Access-Control-Request-Method} request header field name.
         * @see <a href="http://www.w3.org/TR/cors/">CORS W3C recommendation</a>
         */
        public static String ACCESS_CONTROL_REQUEST_METHOD = "Access-Control-Request-Method";
        /**
         * The HTTP {@code Age} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7234#section-5.1">Section 5.1 of RFC 7234</a>
         */
        public static String AGE = "Age";
        /**
         * The HTTP {@code Allow} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-7.4.1">Section 7.4.1 of RFC 7231</a>
         */
        public static String ALLOW = "Allow";
        /**
         * The HTTP {@code Authorization} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7235#section-4.2">Section 4.2 of RFC 7235</a>
         */
        public static String AUTHORIZATION = "Authorization";
        /**
         * The HTTP {@code Cache-Control} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7234#section-5.2">Section 5.2 of RFC 7234</a>
         */
        public static String CACHE_CONTROL = "Cache-Control";
        /**
         * The HTTP {@code Connection} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-6.1">Section 6.1 of RFC 7230</a>
         */
        public static String CONNECTION = "Connection";
        /**
         * The HTTP {@code Content-Encoding} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-3.1.2.2">Section 3.1.2.2 of RFC 7231</a>
         */
        public static String CONTENT_ENCODING = "Content-Encoding";
        /**
         * The HTTP {@code Content-Disposition} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc6266">RFC 6266</a>
         */
        public static String CONTENT_DISPOSITION = "Content-Disposition";
        /**
         * The HTTP {@code Content-Language} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-3.1.3.2">Section 3.1.3.2 of RFC 7231</a>
         */
        public static String CONTENT_LANGUAGE = "Content-Language";
        /**
         * The HTTP {@code Content-Length} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-3.3.2">Section 3.3.2 of RFC 7230</a>
         */
        public static String CONTENT_LENGTH = "Content-Length";
        /**
         * The HTTP {@code Content-Location} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-3.1.4.2">Section 3.1.4.2 of RFC 7231</a>
         */
        public static String CONTENT_LOCATION = "Content-Location";
        /**
         * The HTTP {@code Content-Range} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7233#section-4.2">Section 4.2 of RFC 7233</a>
         */
        public static String CONTENT_RANGE = "Content-Range";
        /**
         * The HTTP {@code Content-Type} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-3.1.1.5">Section 3.1.1.5 of RFC 7231</a>
         */
        public static String CONTENT_TYPE = "Content-Type";
        /**
         * The HTTP {@code Cookie} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc2109#section-4.3.4">Section 4.3.4 of RFC 2109</a>
         */
        public static String COOKIE = "Cookie";
        /**
         * The HTTP {@code Date} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-7.1.1.2">Section 7.1.1.2 of RFC 7231</a>
         */
        public static String DATE = "Date";
        /**
         * The HTTP {@code ETag} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-2.3">Section 2.3 of RFC 7232</a>
         */
        public static String ETAG = "ETag";
        /**
         * The HTTP {@code Expect} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-5.1.1">Section 5.1.1 of RFC 7231</a>
         */
        public static String EXPECT = "Expect";
        /**
         * The HTTP {@code Expires} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7234#section-5.3">Section 5.3 of RFC 7234</a>
         */
        public static String EXPIRES = "Expires";
        /**
         * The HTTP {@code From} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-5.5.1">Section 5.5.1 of RFC 7231</a>
         */
        public static String FROM = "From";
        /**
         * The HTTP {@code Host} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-5.4">Section 5.4 of RFC 7230</a>
         */
        public static String HOST = "Host";
        /**
         * The HTTP {@code If-Match} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-3.1">Section 3.1 of RFC 7232</a>
         */
        public static String IF_MATCH = "If-Match";
        /**
         * The HTTP {@code If-Modified-Since} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-3.3">Section 3.3 of RFC 7232</a>
         */
        public static String IF_MODIFIED_SINCE = "If-Modified-Since";
        /**
         * The HTTP {@code If-None-Match} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-3.2">Section 3.2 of RFC 7232</a>
         */
        public static String IF_NONE_MATCH = "If-None-Match";
        /**
         * The HTTP {@code If-Range} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7233#section-3.2">Section 3.2 of RFC 7233</a>
         */
        public static String IF_RANGE = "If-Range";
        /**
         * The HTTP {@code If-Unmodified-Since} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-3.4">Section 3.4 of RFC 7232</a>
         */
        public static String IF_UNMODIFIED_SINCE = "If-Unmodified-Since";
        /**
         * The HTTP {@code Last-Modified} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7232#section-2.2">Section 2.2 of RFC 7232</a>
         */
        public static String LAST_MODIFIED = "Last-Modified";
        /**
         * The HTTP {@code Link} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc5988">RFC 5988</a>
         */
        public static String LINK = "Link";
        /**
         * The HTTP {@code Location} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-7.1.2">Section 7.1.2 of RFC 7231</a>
         */
        public static String LOCATION = "Location";
        /**
         * The HTTP {@code Max-Forwards} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-5.1.2">Section 5.1.2 of RFC 7231</a>
         */
        public static String MAX_FORWARDS = "Max-Forwards";
        /**
         * The HTTP {@code Origin} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc6454">RFC 6454</a>
         */
        public static String ORIGIN = "Origin";
        /**
         * The HTTP {@code Pragma} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7234#section-5.4">Section 5.4 of RFC 7234</a>
         */
        public static String PRAGMA = "Pragma";
        /**
         * The HTTP {@code Proxy-Authenticate} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7235#section-4.3">Section 4.3 of RFC 7235</a>
         */
        public static String PROXY_AUTHENTICATE = "Proxy-Authenticate";
        /**
         * The HTTP {@code Proxy-Authorization} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7235#section-4.4">Section 4.4 of RFC 7235</a>
         */
        public static String PROXY_AUTHORIZATION = "Proxy-Authorization";
        /**
         * The HTTP {@code Range} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7233#section-3.1">Section 3.1 of RFC 7233</a>
         */
        public static String RANGE = "Range";
        /**
         * The HTTP {@code Referer} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-5.5.2">Section 5.5.2 of RFC 7231</a>
         */
        public static String REFERER = "Referer";
        /**
         * The HTTP {@code Retry-After} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-7.1.3">Section 7.1.3 of RFC 7231</a>
         */
        public static String RETRY_AFTER = "Retry-After";
        /**
         * The HTTP {@code Server} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-7.4.2">Section 7.4.2 of RFC 7231</a>
         */
        public static String SERVER = "Server";
        /**
         * The HTTP {@code Set-Cookie} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc2109#section-4.2.2">Section 4.2.2 of RFC 2109</a>
         */
        public static String SET_COOKIE = "Set-Cookie";
        /**
         * The HTTP {@code Set-Cookie2} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc2965">RFC 2965</a>
         */
        public static String SET_COOKIE2 = "Set-Cookie2";
        /**
         * The HTTP {@code TE} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-4.3">Section 4.3 of RFC 7230</a>
         */
        public static String TE = "TE";
        /**
         * The HTTP {@code Trailer} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-4.4">Section 4.4 of RFC 7230</a>
         */
        public static String TRAILER = "Trailer";
        /**
         * The HTTP {@code Transfer-Encoding} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-3.3.1">Section 3.3.1 of RFC 7230</a>
         */
        public static String TRANSFER_ENCODING = "Transfer-Encoding";
        /**
         * The HTTP {@code Upgrade} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-6.7">Section 6.7 of RFC 7230</a>
         */
        public static String UPGRADE = "Upgrade";
        /**
         * The HTTP {@code User-Agent} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-5.5.3">Section 5.5.3 of RFC 7231</a>
         */
        public static String USER_AGENT = "User-Agent";
        /**
         * The HTTP {@code Vary} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7231#section-7.1.4">Section 7.1.4 of RFC 7231</a>
         */
        public static String VARY = "Vary";
        /**
         * The HTTP {@code Via} header field name.
         * @see <a href="http://tools.ietf.org/html/rfc7230#section-5.7.1">Section 5.7.1 of RFC 7230</a>
         */
        public static String VIA = "Via";



        public void set(String headerName, String headerValue)
        {
            List<String> headerValues = new List<String>();
            headerValues.Add(headerValue);
            this.headers.Add(headerName, headerValues);
        }

    }
}
