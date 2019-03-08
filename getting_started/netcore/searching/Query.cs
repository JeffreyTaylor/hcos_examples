using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace hcos.getting_started.searching
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Query
    {
        [JsonProperty("criterion",Required = Required.Always)]
        public string Criterion { get; set; }
    }
}