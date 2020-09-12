// Copyright (C) 2016 by Barend Erasmus and donated to the public domain

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SimpleHttpServer.Models
{
    public class Route
    {
        public string Name { get; set; } // descriptive name for debugging
        
        public string Method { get; set; }
        public bool isRegex { get; set; } = false;

        public object instance { get; set; }
        public MethodInfo method { get; set; }

        public object Handler(HttpRequest request) {
            return method.Invoke(instance, new object[] { request });
        }

        public string Path{ get; set; }
        
    }
}
