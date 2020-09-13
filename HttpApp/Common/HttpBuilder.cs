using RestServer.Http;
using System.IO;

namespace RestServer.Common
{
    class HttpBuilder
    {
        public static HttpResponse InternalServerError()
        {
            string content = File.ReadAllText("Resources/Pages/500.html"); 

            return new HttpResponse()
            {
                StatusDescription = "InternalServerError",
                StatusCode = "500",
                ContentAsUTF8 = content
            };
        }

        public static HttpResponse NotFound()
        {
            //string content = File.ReadAllText("Resources/Pages/404.html");

            return new HttpResponse()
            {
                StatusDescription = "NotFound",
                StatusCode = "404",
                ContentAsUTF8 = ""
            };
        }

        public static HttpResponse NotAllowed()
        {
            return new HttpResponse()
            {
                StatusDescription = "Method Not Allowed",
                StatusCode = "405",
                ContentAsUTF8 = ""
            };
        }

        public static HttpResponse Ok()
        {
            return new HttpResponse()
            {
                StatusDescription = "Ok",
                StatusCode = "200",
                ContentAsUTF8 = ""
            };
        }

        public static HttpResponse Ok(string content)
        {
            return new HttpResponse()
            {
                StatusDescription = "Ok",
                StatusCode = "200",
                ContentAsUTF8 = content
            };
        }


        public static void NotFound(ref HttpResponse response)
        {
            NotFound(ref response, "");
        }
        public static void NotFound(ref HttpResponse response, string message)
        {
            response.StatusDescription = "NotFound";
            response.StatusCode = "404";
            response.ContentAsUTF8 = message;
        }
        
        public static void NotAllowed(ref HttpResponse response)
        {
            NotAllowed(ref response, "");
        }
        public static void NotAllowed(ref HttpResponse response, string message)
        {
            response.StatusDescription = "Method Not Allowed";
            response.StatusCode = "405";
            response.ContentAsUTF8 = message;
        }
        
        public static void Ok(ref HttpResponse response, string content)
        {
            response.StatusDescription = "Ok";
            response.StatusCode = "200";
            response.ContentAsUTF8 = content;
        }

        public static void InternalServerError(ref HttpResponse response)
        {
            response.StatusDescription = "InternalServerError";
            response.StatusCode = "500";
            response.ContentAsUTF8 = "";
        }
    }
}
