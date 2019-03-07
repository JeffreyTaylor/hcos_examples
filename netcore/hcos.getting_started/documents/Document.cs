using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hcos.getting_started.documents
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Document
    {
        [JsonProperty(Required = Required.Always)]
        public string root { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? date_of_service { get; set; }
        [JsonProperty(Required = Required.Always)]
        public DateTime source_created_at { get; set; }
        [JsonProperty(Required = Required.Always)]
        public DateTime source_updated_at { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string status { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string type_root { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string type_extension { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string type_description { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string confidentiality_code { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Person[] people { get; set; }
    }
}
