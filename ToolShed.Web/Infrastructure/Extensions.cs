using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolShed.Web.Infrastructure
{
    public static class Extensions
    {
        public static string PathAndQueryString(this HttpRequest request)
        {

            if (request.QueryString.HasValue)
            {
                return $"{request.Path}{request.QueryString}";
            }

            return request.Path.ToString();

        }

        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            if (sessionData == null) return default(T);

            return JsonConvert.DeserializeObject<T>(sessionData);
        }


    }
}
