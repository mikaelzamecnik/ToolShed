using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolShed.Web.Models;

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

        public static List<SelectListItem> ToSelectList(this IEnumerable<Category> categories, Product p = null)
        {
            var list = categories.Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == p?.CategoryId)

                }).ToList();
            return list;
                

        }


    }
}
