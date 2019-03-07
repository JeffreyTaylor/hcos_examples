using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace hcos.getting_started.searching
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DocumentEntry
    {
        [JsonProperty("kamioka_updated_at", Required = Required.Always)]
        public DateTime KamiokaUpdatedAt { get; set; }
        [JsonProperty("patient_root", Required = Required.Always)]
        public string PatientRoot { get; set; }
        [JsonProperty("patient_extension", Required = Required.Always)]
        public string PatientExtension { get; set; }
        [JsonProperty("patient_cluster_id", Required = Required.AllowNull)]
        public string PatientClusterId { get; set; }
        [JsonProperty("patient_last_name", Required = Required.Always)]
        public string PatientLastName { get; set; }
        [JsonProperty("patient_first_name", Required = Required.Always)]
        public string PatientFirstName { get; set; }
        [JsonProperty("patient_middle_name", Required = Required.Always)]
        public string PatientMiddle_Name { get; set; }
        [JsonProperty("patient_birthdate", Required = Required.Always)]
        public DateTime PatientBirthdate { get; set; }
        [JsonProperty("patient_age_at_document_date", Required = Required.Always)]
        public int PatientAgeAtDocumentDate { get; set; }
        [JsonProperty("patient_gender", Required = Required.Always)]
        public string PatientGender { get; set; }
        [JsonProperty("document_root", Required = Required.Always)]
        public string DocumentRoot { get; set; }
        [JsonProperty("document_extension", Required = Required.Always)]
        public string DocumentExtension { get; set; }
        [JsonProperty("document_type_root", Required = Required.Always)]
        public string Document_type_root { get; set; }
        [JsonProperty("document_type_extension", Required = Required.Always)]
        public string DocumentTypeExtension { get; set; }
        [JsonProperty("facility_root", Required = Required.Always)]
        public string FacilityRoot { get; set; }
        [JsonProperty("facility_extension", Required = Required.Always)]
        public string FacilityExtension { get; set; }
        [JsonProperty("Document_kind_of_document_root", Required = Required.AllowNull)]
        public string DocumentKindOfDocumentRoot { get; set; }
        [JsonProperty("document_kind_of_document_extension", Required = Required.AllowNull)]
        public string DocumentKindOfDocumentExtension { get; set; }
        [JsonProperty("document_type_of_service_root", Required = Required.AllowNull)]
        public string DocumentTypeOfServiceRoot { get; set; }
        [JsonProperty("document_type_of_service_extension", Required = Required.AllowNull)]
        public string DocumentTypeOfServiceExtension { get; set; }
        [JsonProperty("document_subject_matter_domain_root", Required = Required.AllowNull)]
        public string DocumentSubjectMatterDomainRoot { get; set; }
        [JsonProperty("document_subject_matter_domain_extension", Required = Required.AllowNull)]
        public string DocumentSubjectMatterDomainExtension { get; set; }
        [JsonProperty("modality", Required = Required.AllowNull)]
        public string Modality { get; set; }
        [JsonProperty("body_location", Required = Required.AllowNull)]
        public string BodyLocation { get; set; }
        [JsonProperty("source_created_at", Required = Required.Always)]
        public DateTime SourceCreatedAt { get; set; }
        [JsonProperty("source_updated_at", Required = Required.Always)]
        public DateTime SourceUpdatedAt { get; set; }
    }
}
