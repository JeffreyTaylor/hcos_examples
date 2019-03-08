using System;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace hcos.getting_started.documents
{
    public static class DocumentsIngestionDemo{
        public static void Demo(string[] args){
            StreamReader streamReader = null;

            try
            {
                Console.WriteLine("Documents bulk load demo begins...");
                documents.Configuration configuration = JsonConvert.DeserializeObject<documents.Configuration>(File.ReadAllText("../../configurations/Configuration.Documents.json"));
                // Documents tenant configuration info
    
                string sourceFilename = "../../data/PatientDocument.bulk.json";
                IRestClient client = new RestClient(configuration.BaseUrl)
                {
                    Authenticator = new documents.Authenticator(configuration.AppId, configuration.AppSecret, configuration.TenantSecret)
                };

                // Create reusable Restsharp request
                IRestRequest request = new RestRequest("/api/v1/patient_document", Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.JsonSerializer = new JsonNetSerializer();

                // Adding hcOS Document Api required query parameters
                request.AddQueryParameter("user[root]", "UserRoot");
                request.AddQueryParameter("user[extension]", "UserExtension");
                request.AddQueryParameter("tid", configuration.TenantId);

                // Adding Content-Type
                request.AddHeader("Content-Type","application/json");
                streamReader = new StreamReader(sourceFilename);
                string pdLine = null;
                IRestResponse response = null;
                documents.PatientDocument pd = null;
                Results results = new Results();
                do
                {
                    pdLine = streamReader.ReadLine();
                    if ((pdLine != null) && (pdLine.Length > 0))
                    {
                        results.Total++;
                        pd = JsonConvert.DeserializeObject<documents.PatientDocument>(pdLine);
                        request.AddOrUpdateParameter("application/json",JsonConvert.SerializeObject(pd),ParameterType.RequestBody);
                        response = client.Execute(request);
                        if ((response.ResponseStatus == ResponseStatus.Completed) && (response.StatusCode == HttpStatusCode.Created))
                        {
                            results.Success++;
                        }
                        else
                        {
                            results.Errors++;
                            Console.WriteLine($"OOPS something wrong.{System.Environment.NewLine}\tResponseStatus: {response.ResponseStatus}{System.Environment.NewLine}\tHttpStatusCode: {response.StatusCode}{System.Environment.NewLine}\tContent: {response.Content}");
                        }
                        Console.WriteLine(results.CurrentStats);
                    }
                } while (streamReader.EndOfStream == false);
                Console.WriteLine("Documents bulk load demo ends...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled exception.\n{ex.ToString()}");
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
            }
        }
    }
}