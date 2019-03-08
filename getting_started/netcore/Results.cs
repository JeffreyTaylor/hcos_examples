using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hcos.getting_started
{
    /// <summary>
    /// Results aggregation.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Results
    {
        /// <summary>
        /// Default contructor.
        /// </summary>
        public Results()
        {
            Total = 0;
            Success = 0;
            Warnings = 0;
            Errors = 0;
        }
        /// <summary>
        /// Total processing count.
        /// </summary>
        [JsonProperty("total", Required = Required.Always)]
        public int Total { get; set; }
        /// <summary>
        /// Success count.
        /// </summary>
        [JsonProperty("success", Required = Required.Always)]
        public int Success { get; set; }
        /// <summary>
        /// Warnings count.
        /// </summary>
        [JsonProperty("warnings", Required = Required.Always)]
        public int Warnings { get; set; }
        /// <summary>
        /// Errors count.
        /// </summary>
        [JsonProperty("errors", Required = Required.Always)]
        public int Errors { get; set; }
        /// <summary>
        /// Current stats.
        /// </summary>
        public string CurrentStats { get { return $"Results T: {Total}, S: {Success}, W: {Warnings} E: {Errors}"; } }
    }
}