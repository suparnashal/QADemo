using APITesting.API.DropBoxApi;
using APITesting.API.DropBoxApi.Models;
using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;
using System.IO;
using System.Net;


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
            MetaDataRequestBody metadata = new MetaDataRequestBody() { path = "/DI/ForRenuka.rtf" };
            restRequest.AddJsonBody(metadata); ;
                        
            IRestResponse<FileMetaData> restResponse = restClient.Post<FileMetaData>(restRequest);

            Assert.AreEqual(200, (int)restResponse.StatusCode);
            Assert.IsTrue(restResponse.Data.is_downloadable);
            Assert.Greater(2000, restResponse.Data.size);
            //Assert.AreEqual("file", restResponse.Data.tag);
        }

        [Test]
        public void Test_DownloadFile()
        {

        }

        [Test]
        public void Test_ListFolder()
        {
            IRestClient restClient = new RestClient();
            restClient.FailOnDeserializationError = true;
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

    }
}