using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VirtualRoom.Logic
{
    public static class ExtensionMethods
    {
        public static List<Guid> ParseUsers(JObject obj)
        {
            var tempObj = obj["users"];
            List<Guid> guids = new List<Guid>();
            if (tempObj.Children().Count()!=0)
            {
                var children = tempObj.Children();
                foreach(var child in children)
                {
                    var value = child.Value<string>();
                    Guid id = Guid.Empty;
                    if(Guid.TryParse(value, out id))
                    {
                        guids.Add(id);
                    }
                }
            }
            return guids;
        }
    }
}