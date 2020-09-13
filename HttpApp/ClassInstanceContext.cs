using System;
using System.Collections.Generic;
using System.Linq;

namespace HttpApp
{
    public class ClassInstanceContext
    {
        public static Dictionary<string, object> map = new Dictionary<string, object>();

        public static object GetInstance(Type t) {
            if (map.Keys.Contains(t.FullName))
            {
                return map[t.FullName];
            }

            object instance = Activator.CreateInstance(t);
            map.Add(t.FullName, instance);
            return instance;
        }
    }
}
