using System;
using System.Collections.Generic;
using System.Web;

namespace RestServer.Http
{
    public class QueryParameter
    {
        public Dictionary<string, string> map;

        public string this[string key] {
            get
            {
                if (map.ContainsKey(key))
                {
                    return map[key];
                }
                else {
                    return null;
                }
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
            if (queryString == null || queryString == String.Empty) {
                return;
            }
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
                if (p[1] != null && p[1] != "")
                {
                    map.Add(p[0], HttpUtility.UrlDecode(p[1]));
                }
                else
                {
                    map.Add(p[0], p[1]);
                }
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
        public bool? GetBoolValue(string key)
        {
            if (map.ContainsKey(key))
            {
                return bool.Parse(map[key]);
            }
            return null;
        }
        public double? GetDoubleValue(string key)
        {
            if (map.ContainsKey(key))
            {
                return double.Parse(map[key]);
            }
            return null;
        }
        public long? GetLongValue(string key)
        {
            if (map.ContainsKey(key))
            {
                return long.Parse(map[key]);
            }
            return null;
        }

    }
}
