using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hcos.getting_started.documents
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
        [JsonProperty("tenantSecret")]
        public string TenantSecret { get; set; }
        public IRestRequest NewRequest(string resource, Method method, bool includeQueryParameters = true)
        {
            IRestRequest request = new RestRequest(resource, method);
            request.RequestFormat = DataFormat.Json;

            request.AddQueryParameter("user[root]", "UserRoot");
            request.AddQueryParameter("user[extension]", "UserExtension");
            request.AddQueryParameter("tid", TenantId);

            return request;
        }
    }
}
