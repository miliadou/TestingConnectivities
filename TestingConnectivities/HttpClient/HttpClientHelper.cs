using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestingConnectivities
{
    public class HttpClientHelper
    {
        private string _url;
        private ILogger _logger;

        public HttpClientHelper(string url, ILogger logger)
        {
            _url = url;
            _logger = logger;
        }

        public async Task<Object> CreateClient() 
        {
            try
            {
                var client = new RestClient(_url);
                var request = new RestRequest();
                var response = await client.ExecuteAsync(request);

                if (response.ErrorException != null)
                {
                    _logger.LogInformation("Call to Salesforce endpoint failed with " + response.ErrorException + " " + response.ErrorMessage);
                    return null;
                }

                var data = JsonConvert.DeserializeObject(response.Content);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogTrace("Restclient for Salesforce communication failed with " + ex.Message);
                return null;
            }
        }        
    }
}
