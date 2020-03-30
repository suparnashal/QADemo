using APITesting.API.DropBoxApi;
using APITesting.API.DropBoxApi.Models;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;
using System.IO;
using System.Net;
using APITesting.API.Framework;


namespace APITesting
{
   [TestFixture]
    public class Tests
    {
        string baseAddress = "https://api.dropboxapi.com";
        string contentBaseAddress = "https://content.dropboxapi.com";
        string authToken = "O0GXRfyla_AAAAAAAAAADbqcrLP4cZUgsNwfzfVy5HVaubNkCPrEoSPHx1enZgC1";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_GetMetaData()
        {            
            IRestClient restClient = new RestClient();
         
            IRestRequest restRequest = new RestRequest($"{baseAddress}/2/files/get_metadata");
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Authorization", $"Bearer {authToken}");
            restRequest.AddHeader("User-Agent", "api-explorer-client");            
            string path = "{\"path\":\"/DI/ForRenuka.rtf\"}";
            restRequest.AddJsonBody(path); ;
            
                        
            IRestResponse<FileMetaData> restResponse = restClient.Post<FileMetaData>(restRequest);

            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsTrue(restResponse.Data.is_downloadable);
            Assert.Greater(2000, restResponse.Data.size);
            //Assert.AreEqual("file", restResponse.Data.tag);
        }

        [Test]
        public void Test_GetMetaData_UsingHelper()
        {
            string path = "{\"path\":\"/DI/ForRenuka.rtf\"}";

            var headers = new Dictionary<string, string>()
            {
                { "Content-Type", "application/json"},
                { "Accept", "application/json"},
                { "Authorization", $"Bearer {authToken}" },
                { "User-Agent", "api-explorer-client"}
            };

            RestSharpHelper restsharphelper = new RestSharpHelper("/2/files/get_metadata", headers, Method.POST, path);
            
            IRestResponse restResponse = restsharphelper.ExecutePostAsync();

            Assert.AreEqual(200, (int)restResponse.StatusCode);          
        }

        [Test]
        public void Test_DownloadFile()
        {
            IRestClient restClient = new RestClient();
            
            IRestRequest restRequest = new RestRequest($"{contentBaseAddress}/2/files/download");

            restRequest.AddHeader("Authorization", $"Bearer {authToken}");
            restRequest.AddHeader("User-Agent", "api-explorer-client");
            restRequest.AddHeader("Dropbox-API-Arg", "{\"path\":\"/DI/ForRenuka.rtf\"}");         
            restRequest.RequestFormat = DataFormat.Json;
            IRestResponse restResponse = restClient.Post(restRequest);

            string downloadedFile = @"C:\ss_carxrm\MyTraining\My QA Projects\QADemo\APITesting\APITesting\API\DownloadedFiles\ForRenuka.rtf";
            WriteToFile(downloadedFile, restResponse.Content);                
            Assert.AreEqual(200, (int)restResponse.StatusCode);
        }

        [Test]
        public void Test_ListFolder()
        {
            IRestClient restClient = new RestClient();            
            IRestRequest restRequest = new RestRequest($"{baseAddress}/2/files/list_folder");
                        
            restRequest.AddHeader("Authorization", $"Bearer {authToken}");            
            restRequest.AddHeader("Content-Type", "application/json");
            MetaDataRequestBody metadata = new MetaDataRequestBody() { path = "/di" };
            restRequest.AddJsonBody(metadata); ;
            restRequest.RequestFormat = DataFormat.Json;
            IRestResponse<ListOfFiles> restResponse = restClient.Post<ListOfFiles>(restRequest);

            List<FileMetaData> folderDetails = restResponse.Data.entries;
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsTrue(folderDetails.Exists((x) => x.name.Contains("Instant challenge")));                  
         }

        private void WriteToFile(string path,string content)
        {
            if (File.Exists(path))
                File.Delete(path);
            
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(content);                    
                }            

        }

    }
}