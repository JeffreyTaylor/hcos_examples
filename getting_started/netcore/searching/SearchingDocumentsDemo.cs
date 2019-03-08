using System;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace hcos.getting_started.searching{
    public static class SearchingDocumentsDemo{
        public static void Demo(string[] args){
              Console.WriteLine("hcOS demo begins...");

            // Making sure Directory Exist
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }
            //Initializing Documents
            documents.Configuration documentsConfiguration = JsonConvert.DeserializeObject<documents.Configuration>(File.ReadAllText("../../configurations/Configuration.Documents.json"));
            IRestClient documentsClient = new RestClient(documentsConfiguration.BaseUrl);
            documentsClient.Authenticator = new documents.Authenticator(documentsConfiguration.AppId, documentsConfiguration.AppSecret, documentsConfiguration.TenantSecret);

            //Initializing Searcj
            searching.Configuration searchConfiguration = JsonConvert.DeserializeObject<searching.Configuration>(File.ReadAllText("../../configurations/Configuration.Searching.json"));
            IRestClient iSearchClient = new RestClient(searchConfiguration.BaseUrl);
            iSearchClient.Authenticator = new searching.Authenticator(searchConfiguration.AppId, searchConfiguration.AppSecret, searchConfiguration.TenantId);

            // Creating search Search request
            IRestRequest iSearchRequest = searchConfiguration.NewRequest($"/api/v1/{searchConfiguration.TenantId}/document/search", Method.POST);
            string literals = "ulcerative colitis";
            Console.WriteLine($"Searching for documents containing '{literals}'");
            iSearchRequest.AddJsonBody(new searching.Query()
            {
                Criterion = $"literal='{literals}'"
            });

            // Making Search Request
            IRestResponse iSearchResponse = iSearchClient.Execute(iSearchRequest);
            if ((iSearchResponse.StatusCode == System.Net.HttpStatusCode.OK) && (iSearchResponse.ResponseStatus == ResponseStatus.Completed))
            {
                searching.QueryResponse queryResponse = JsonConvert.DeserializeObject<searching.QueryResponse>(iSearchResponse.Content);
                Console.WriteLine($"searchResponse.Offset: {queryResponse.Offset}");
                Console.WriteLine($"searchResponse.RecordCount: {queryResponse.RecordCount}");
                Console.WriteLine($"searchResponse.TotalRecordCount: {queryResponse.TotalRecordCount}");
                int count = 1;
                string outFileName;
                foreach (searching.DocumentEntry entry in queryResponse.Hits)
                {
                    Console.WriteLine($"{count}) {entry.DocumentRoot}-{entry.DocumentExtension}-{entry.DocumentTypeExtension}");
                    IRestRequest documentsRequest = documentsConfiguration.NewRequest($"/api/v1/patient_document/{entry.DocumentRoot}/{entry.DocumentExtension}/text", Method.GET);
                    IRestResponse documentsResponse = documentsClient.Execute(documentsRequest);
                    if ((documentsResponse.StatusCode == System.Net.HttpStatusCode.OK) && (documentsResponse.ResponseStatus == ResponseStatus.Completed))
                    {
                        outFileName = $"./data/{entry.DocumentRoot}.{entry.DocumentExtension}.txt";
                        File.WriteAllText(outFileName, documentsResponse.Content);
                        Console.WriteLine($"Saved to {outFileName}");
                    }
                    else
                    {
                        Console.WriteLine($"documentsResponse.StatusCode: {documentsResponse.StatusCode}");
                        Console.WriteLine($"documentsResponse.ResponseStatus: {documentsResponse.ResponseStatus}");
                        Console.WriteLine($"documentsResponse.Content:{System.Environment.NewLine}{documentsResponse.Content}");
                        Console.WriteLine($"Halting...");
                        return;
                    }
                    count++;
                }
            }
            else
            {
                Console.WriteLine($"searchResponse.StatusCode: {iSearchResponse.StatusCode}");
                Console.WriteLine($"searchResponse.ResponseStatus: {iSearchResponse.ResponseStatus}");
                Console.WriteLine($"searchResponse.Content:{System.Environment.NewLine}{iSearchResponse.Content}");
                Console.WriteLine($"Halting...");
                return;
            }
            Console.WriteLine("hcOS demo ends...");
        }
    }
}