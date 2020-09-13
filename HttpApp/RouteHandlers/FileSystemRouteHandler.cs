
using HttpApp.Http;
using HttpApp.RouteHandlers;
using SimpleHttpServer.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace SimpleHttpServer.RouteHandlers
{
    public class FileSystemRouteHandler : IRouteHandler
    {
        public string Prefix { get; set; } = "/static";
        public string BasePath { get; set; } = AppDomain.CurrentDomain.BaseDirectory;
        public bool ShowDirectories { get; set; }

        public void Handler(HttpRequest request, ref HttpResponse response) {
            var url_part = request.Path;

            url_part = url_part.Replace("\\..\\", "\\");
            url_part = url_part.Replace("/../", "/");
            url_part = url_part.Replace("//","/");
            url_part = url_part.Replace(@"\\",@"\");
            url_part = url_part.Replace(":","");           
            url_part = url_part.Replace("/",Path.DirectorySeparatorChar.ToString());
           
            if (url_part.Length > 0) {
                var first_char = url_part.ElementAt(0);
                if (first_char == '/' || first_char == '\\') {
                    url_part = "." + url_part;
                }
            }
            var local_path = Path.Combine(this.BasePath, url_part);
                            
            if (ShowDirectories && Directory.Exists(local_path)) {
                HandlDir(request,ref response,local_path);
            } else if (File.Exists(local_path)) {
                HandleFile(request,ref response, local_path);
            } else {
                HttpBuilder.NotFound(ref response, string.Format("Not Found ({0}) handler", local_path));
            }
        }

        private void HandleFile(HttpRequest request, ref HttpResponse response, string local_path) {        
            var file_extension = Path.GetExtension(local_path);

            response.StatusCode = "200";
            response.StatusDescription = "Ok";
            response.Headers["Content-Type"] = MimeType.GetOrDefault(file_extension, "application/octet-stream");
            response.Content = File.ReadAllBytes(local_path);

        }

        private void HandlDir(HttpRequest request, ref HttpResponse response, string local_path) {
            var output = new StringBuilder();
            output.Append(string.Format("<h1> Directory: {0} </h1>",request.Url));
                        
            foreach (var entry in Directory.GetFiles(local_path)) {                
                var file_info = new FileInfo(entry);

                var filename = file_info.Name;
                output.Append(string.Format("<a href=\"{1}\">{1}</a> <br>",filename,filename));                
            }
            HttpBuilder.Ok(ref response, output.ToString());

        }

        public bool IsMatch(HttpRequest request)
        {
            return request.Path.StartsWith(Prefix);
        }
    }


}
