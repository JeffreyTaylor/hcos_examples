using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hcos.getting_started.documents
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Patient
    {
        [JsonProperty(Required = Required.Always)]
        public string root { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string extension { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string last_name { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string first_name { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Include)]
        public string middle_name { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string gender { get; set; }
        [JsonProperty(Required = Required.Always)]
        public DateTime date_of_birth { get; set; }
    }
}
