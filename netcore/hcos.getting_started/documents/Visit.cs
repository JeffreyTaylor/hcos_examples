using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hcos.getting_started.documents
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Visit
    {
        [JsonProperty(Required = Required.Always)]
        public string root { get; set; }
        [JsonProperty(Required = Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string extension { get; set; }
        [JsonProperty(Required = Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public DateTime admitted_at { get; set; }
        [JsonProperty(Required = Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public DateTime discharged_at { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Include)]
        public Facility facility { get; set; }
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public Person[] people { get; set; }
    }
}
