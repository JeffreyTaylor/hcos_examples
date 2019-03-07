
using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace hcos.getting_started.documents{
    public static class DocumentsDemo{
        public static void Demo(string[] args){
            Console.WriteLine("Documents demo begins...");
            documents.Configuration configuration = JsonConvert.DeserializeObject<documents.Configuration>(File.ReadAllText("../../configurations/Configuration.Documents.json"));
            // Documents tenant configuration info
            string documentRoot = "MIMIC.DOCUMENT.OID";
            string documentExtension = "1";

            IRestClient client = new RestClient(configuration.BaseUrl)
            {
                Authenticator = new documents.Authenticator(configuration.AppId, configuration.AppSecret, configuration.TenantSecret)
            };

            // Create Restsharp request
            //Getting current document for given document root&extension

            IRestRequest request = configuration.NewRequest($"/api/v1/patient_document/{documentRoot}/{documentExtension}", Method.GET);

            IRestResponse response = client.Execute(request);

            if ((response.ResponseStatus == ResponseStatus.Completed) && (response.StatusCode == HttpStatusCode.OK))
            {
                documents.MinimalPatientDocument mPD = JsonConvert.DeserializeObject<documents.MinimalPatientDocument>(response.Content);

                Console.WriteLine($"mPD.document_root: {mPD.document_root}");
                Console.WriteLine($"mPD.document_extension: {mPD.document_extension}");
                Console.WriteLine($"mPD.document_type-description: {mPD.document_type_description}");
            }
            else
            {
                Console.WriteLine($"OOPS something wrong.{System.Environment.NewLine}\tResponseStatus: {response.ResponseStatus}{System.Environment.NewLine}\tHttpStatusCode: {response.StatusCode}{System.Environment.NewLine}\tContent: {response.Content}");
            }
            Console.WriteLine("Documents demo ends...");

        }
    }
}