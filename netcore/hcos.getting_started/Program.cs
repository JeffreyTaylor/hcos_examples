using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace hcos.getting_started
{
    class Program
    {        
        
        static void Main(string[] args)
        {
            if (args.Length == 0) {
                GettingStartedDemo.Demo(args);
            }
            else {
                switch (args[0])
                {
                    case "documents_demo":
                        documents.DocumentsDemo.Demo(args);
                        break;
                    case "documents_ingestion_demo":
                        documents.DocumentsIngestionDemo.Demo(args);
                        break;
                    case "searching_demo":
                        searching.SearchingDemo.Demo(args);
                        break;
                    case "searching_documents_demo":
                        searching.SearchingDocumentsDemo.Demo(args);
                        break;
                }
            }
        }
    }
}
