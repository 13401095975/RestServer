using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.RouteHandlers
{
    public interface IRouteHandler
    {
        bool IsMatch(HttpRequest request);
        void Handler(HttpRequest request,ref HttpResponse response);
    }
}
