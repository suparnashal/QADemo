using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace APITesting.API.Framework
{
    public class RestSharpHelper
    {
        private Dictionary<string, string> parameters;
        private string jsonBody; 
        public static readonly string DropBoxBaseUrl = "https://api.dropboxapi.com";
        private Method method;
        private string resourceUrl; 
        public RestSharpHelper(string _resourceUrl, Dictionary<string, string> _parameters, Method _operation,string _jsonBody)
        {
            parameters = _parameters;
            method = _operation;
            jsonBody = _jsonBody;
            resourceUrl = _resourceUrl;
        }

        private RestRequest CreateRequest()
        {
            RestRequest restRequest = new RestRequest($"{DropBoxBaseUrl}/{resourceUrl}");

            foreach (string key in parameters.Keys)
                restRequest.AddHeader(key, parameters[key]);
            restRequest.AddJsonBody(jsonBody);
            return restRequest;
        }


        public IRestResponse ExecutePostAsync()
        {
            IRestClient restClient = new RestClient();
            IRestRequest restRequest = CreateRequest(); 
            IRestResponse restResponse = restClient.Post(restRequest);
            return restResponse;
        }

    }
}
