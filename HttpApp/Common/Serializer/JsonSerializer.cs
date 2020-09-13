using System;
using System.Web.Script.Serialization;

namespace RestServer.Common.Serializer
{
    public class JsonSerializer
    {
        public static string ToJson(object data) {
            if (data is string || data is int || data is String || data is Int16)
            {
                return data.ToString();
            }
            else
            {
                return new JavaScriptSerializer().Serialize(data);
            }
        }

        public static  T FromJson<T>(string jsonString) { 
            return new JavaScriptSerializer().Deserialize<T>(jsonString);
        }
    }
}
