using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace hcos.getting_started.searching
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Configuration
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }
        [JsonProperty("appId")]
        public string AppId { get; set; }
        [JsonProperty("appSecret")]
        public string AppSecret { get; set; }
        [JsonProperty("tenantId")]
        public string TenantId { get; set; }
        public IRestRequest NewRequest(string resource, Method method)
        {
            IRestRequest request = new RestRequest(resource, method);
            request.RequestFormat = DataFormat.Json;
            request.JsonSerializer = new JsonNetSerializer();
            request.AddOrUpdateParameter("Content-Type", "application/json", ParameterType.HttpHeader);
            request.AddOrUpdateParameter("user_root", "UserRoot", ParameterType.HttpHeader);
            request.AddOrUpdateParameter("user_extension", "UserExtension", ParameterType.HttpHeader);

            return request;
        }
    }
}
