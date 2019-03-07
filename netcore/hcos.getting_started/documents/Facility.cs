using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hcos.getting_started.documents
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Facility
    {
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string extension { get; set; }
    }
}
