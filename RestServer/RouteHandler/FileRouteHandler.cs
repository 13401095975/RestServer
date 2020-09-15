using RestServer.Common;
using RestServer.Config;
using RestServer.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace RestServer.RouteHandler
{
    public class FileRouteHandler : IRouteHandler
    {
        public string Prefix { get; set; } = ServerConfig.DefaultServerRoot;
        public string BaseRoot { get; set; } = ServerConfig.DefaultServerRoot;
        public bool ShowDirectories { get; set; } = true;

        public FileRouteHandler() { 
        }

        public FileRouteHandler(StaticFileConfiguration configuration) {
            this.Prefix = configuration.Prefix;
            this.BaseRoot = configuration.BaseRoot;
            this.ShowDirectories = configuration.ShowDirectories;
        }

        public FileRouteHandler(string Prefix, string BaseRoot, bool ShowDirectories) {
            this.Prefix = Prefix;
            this.BaseRoot = BaseRoot;
            this.ShowDirectories = ShowDirectories;
        }

        public void Handler(HttpRequest request, ref HttpResponse response) {
            var path = request.Path.Substring(Prefix.Length);

            path = path.Replace("\\..\\", "\\");
            path = path.Replace("/../", "/");
            path = path.Replace("//","/");
            path = path.Replace(@"\\",@"\");
            path = path.Replace(":","");           
            path = path.Replace("/",Path.DirectorySeparatorChar.ToString());
           
            if (path.Length > 0) {
                var first_char = path.ElementAt(0);
                if (first_char == '/' || first_char == '\\') {
                    path = path.Substring(1);
                }
            }
            var local_path = Path.Combine(this.BaseRoot, path);
                            
            if (ShowDirectories && Directory.Exists(local_path)) {
                HandlDir(request,ref response,local_path);
            } else if (File.Exists(local_path)) {
                HandleFile(request,ref response, local_path);
            } else {
                HttpBuilder.NotFound(ref response, string.Format("Not Found ({0}) handler", request.Path));
            }
        }

        private void HandleFile(HttpRequest request, ref HttpResponse response, string local_path) {        
            var file_extension = Path.GetExtension(local_path);

            response.StatusCode = "200";
            response.StatusDescription = "Ok";
            response.Headers.SetContentType(MimeType.GetOrDefault(file_extension, "application/octet-stream"));
            response.Data = File.ReadAllBytes(local_path);

        }

        private void HandlDir(HttpRequest request, ref HttpResponse response, string local_path) {
            var output = new StringBuilder();
            output.Append(string.Format("<h3> Directory: {0} </h3>",request.Path));
                        
            foreach (var entry in Directory.GetFiles(local_path)) {                
                var file_info = new FileInfo(entry);

                var filename = file_info.Name;
                output.Append(string.Format("<a href=\"{0}\">{1}</a> <br>", request.Path + Path.DirectorySeparatorChar.ToString() + filename,filename));                
            }

            foreach (var entry in Directory.GetDirectories(local_path))
            {
                var file_info = new FileInfo(entry);

                var filename = file_info.Name;
                output.Append(string.Format("<a href=\"{0}\">{1}</a> <br>", request.Path+Path.DirectorySeparatorChar.ToString()+filename, filename));
            }
            response.Headers.SetContentTypeWithDefaultCharset("text/html");
            HttpBuilder.Ok(ref response, output.ToString());

        }

        public bool IsMatch(HttpRequest request)
        {
            return request.Path.StartsWith(Prefix);
        }
    }


}
