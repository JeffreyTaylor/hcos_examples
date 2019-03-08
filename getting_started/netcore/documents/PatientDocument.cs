using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hcos.getting_started.documents
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PatientDocument
    {
        public PatientDocument()
        {

        }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string source_versioned_at { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string root { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string extension { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string extension_suffix { get; set; }
        [JsonProperty(Required = Required.Always)]
        public DateTime source_last_modified_at { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string data { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string base64_data { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string data_format { get; set; }
        [JsonProperty(Required = Required.Default)]
        public string data_status { get; set; }
        [JsonProperty(Required = Required.Always)]
        public string data_precedence { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Patient patient { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Visit visit { get; set; }
        [JsonProperty(Required = Required.Always)]
        public Document document { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public PatientDocument[] contained_patient_documents { get; set; }
    }
}
