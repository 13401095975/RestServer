using System;
using System.Collections.Generic;

namespace RestServer.Http
{
    public class QueryParameter
    {
        public Dictionary<string, string> map;

        public string this[string key] {
            get
            {
                return map[key];
            }
        }

        public QueryParameter() {
            map = new Dictionary<string, string>();
        }

        public QueryParameter(string queryString)
        {
            map = new Dictionary<string, string>();
            Parse(queryString);
        }

        public void Parse(string queryString) {
            map.Clear();

            string[] list = queryString.Split('&');
            if (list == null || list.Length == 0)
            {
                return;
            }

            foreach (string s in list)
            {
                string[] p = s.Split('=');
                if (p == null || p.Length < 2)
                {
                    continue;
                }
                map.Add(p[0], p[1]);
            }
        }

        public string GetStringValue(string key) {
            if (map.ContainsKey(key)) {
                return map[key];
            }
            return null;
        }

        public int? GetIntValue(string key)
        {
            if (map.ContainsKey(key))
            {
                return int.Parse(map[key]);
            }
            return null;
        }
              
    }
}
