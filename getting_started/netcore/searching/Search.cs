using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace hcos.getting_started.searching
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Search
    {
        [JsonProperty("description",Required = Required.Always)]
        public string Description { get; set; }
        [JsonProperty("query",Required = Required.Always)]
        public Query Query { get; set; }
    }
}
