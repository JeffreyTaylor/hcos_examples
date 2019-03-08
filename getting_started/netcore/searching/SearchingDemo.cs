using System;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace hcos.getting_started.searching{
    public static class SearchingDemo{
        public static void Demo(string[] args){
            Console.WriteLine("Searching begins...");

            // hcOS Document Api tenant configuration info
            Configuration configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText("../../configurations/Configuration.Searching.json"));

            Search[] searches = JArray.Parse(File.ReadAllText("../../data/Searches.json")).ToObject<Search[]>();

            IRestClient client = new RestClient(configuration.BaseUrl);

            client.Authenticator = new searching.Authenticator(configuration.AppId, configuration.AppSecret, configuration.TenantId);

            Results results = new Results();

            IRestRequest request = configuration.NewRequest($"/api/v1/{configuration.TenantId}/document/search", Method.POST);
            IRestResponse response = null;
            foreach (Search search in searches)
            {
                results.Total++;
                Console.WriteLine($"Begin: {search.Description}");
                Console.WriteLine(JsonConvert.SerializeObject(search.Query, Formatting.Indented));
                request.AddOrUpdateParameter("application/json", JsonConvert.SerializeObject(search.Query), ParameterType.RequestBody);
                response = client.Execute(request);
                if ((response.ResponseStatus == ResponseStatus.Completed) && (response.StatusCode == HttpStatusCode.OK))
                {
                    QueryResponse queryResponse = JsonConvert.DeserializeObject<QueryResponse>(response.Content);

                    Console.WriteLine($"searchResponse.Offset: {queryResponse.Offset}");
                    Console.WriteLine($"searchResponse.RecordCount: {queryResponse.RecordCount}");
                    Console.WriteLine($"searchResponse.TotalRecordCount: {queryResponse.TotalRecordCount}");
                    int count = 1;
                    foreach (DocumentEntry entry in queryResponse.Hits)
                    {
                        Console.WriteLine($"{count}) {entry.DocumentRoot}-{entry.DocumentExtension}-{entry.DocumentTypeExtension}");
                        count++;
                    }
                    results.Success++;
                }
                else
                {
                    results.Errors++;
                    Console.WriteLine($"OOPS something wrong.{System.Environment.NewLine}\tResponseStatus: {response.ResponseStatus}{System.Environment.NewLine}\tHttpStatusCode: {response.StatusCode}{System.Environment.NewLine}\tContent: {response.Content}");
                }
                Console.WriteLine($"Ends: {search.Description}{System.Environment.NewLine}");
            }
            Console.WriteLine(results.CurrentStats);
        }
    }
}