using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hcos.getting_started.documents
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MinimalPatientDocument
    {
        public MinimalPatientDocument()
        {

        }

        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string id { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string extension_suffix { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime updated_at { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime created_at { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime date_of_service { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime received_in_repository_at { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string data_format { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string data_status { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string data_precedence { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string patient_root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string patient_extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_status { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_type_root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_type_extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_type_description { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string baseline_document_type_root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string baseline_document_type_extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string baseline_document_type_description { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_type_of_service_root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_type_of_service_extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_type_of_service_description { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_kind_of_document_root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_kind_of_document_extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_subject_matter_domain_root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string document_subject_matter_domain_extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string facility_root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string facility_extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string facility_baseline_root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string facility_baseline_extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string visit_root { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public string visit_extension { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_admitting_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_attending_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_authenticating_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_authoring_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_consulting_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_interpreting_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_ordering_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_performing_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_receiving_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_referring_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_transcribing_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] document_unknown_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_admitting_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_attending_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_authenticating_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_authoring_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_consulting_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_interpreting_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_ordering_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_performing_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_receiving_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_referring_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_transcribing_person { get; set; }
        [JsonProperty(Required = Required.Default, NullValueHandling = NullValueHandling.Ignore)]
        public object[] visit_unknown_person { get; set; }
    }

}
