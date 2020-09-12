using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp.Filter
{
    public interface IFilter
    {
        void Filter(HttpRequest request,ref HttpResponse response, FilterChain next);
    }
}
