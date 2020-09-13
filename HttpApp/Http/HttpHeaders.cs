using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestServer.Http
{
    public class HttpHeaders
    {
        private Dictionary<string, string> Headers { get; set; }

        public string this[string name] {
            get {
                return Headers[name];
            }
        }

        public HttpHeaders() {
            Headers = new Dictionary<string, string>();
        }

        public bool Contains(string key) {
            return Headers.ContainsKey(key);
        }
        public void Add(string key, string value) {
            Headers.Add(key, value);
        }

        public override string ToString()
        {
            return string.Join("\r\n", this.Headers.Select(x => string.Format("{0}: {1}", x.Key, x.Value)));
        }
    }
}
