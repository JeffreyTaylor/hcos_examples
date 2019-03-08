using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hcos.getting_started.searching
{
    [JsonObject(MemberSerialization.OptIn)]
    public class QueryResponse
    {
        [JsonProperty("offset", Required = Required.Always)]
        public int Offset { get; set; }
        [JsonProperty("record_count", Required = Required.Always)]
        public int RecordCount { get; set; }
        [JsonProperty("total_record_count", Required = Required.Always)]
        public int TotalRecordCount { get; set; }
        [JsonProperty("hits", Required = Required.Always)]
        public DocumentEntry[] Hits { get; set; }
    }
}
