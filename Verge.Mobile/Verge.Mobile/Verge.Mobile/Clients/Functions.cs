using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Verge.Mobile.Mobile.Clients
{
    public class Functions
    {
        public Func<object, StringContent> CreateJsonContent => (px) =>
            {
                string content = Newtonsoft.Json.JsonConvert.SerializeObject(px);
                return new StringContent(content, Encoding.UTF8, "application/json");
            };
        public Func<object, StringContent> CreateFormUrlencoded => (px) =>
        {           
            return new StringContent((string)px, Encoding.UTF8, "application/x-www-form-urlencoded");
        };
    }
}
