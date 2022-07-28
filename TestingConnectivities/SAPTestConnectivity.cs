using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace TestingConnectivities
{
    public class SAPTestConnectivity
    {
        private readonly ILogger<SAPTestConnectivity> _logger;

        public SAPTestConnectivity(ILogger<SAPTestConnectivity> log)
        {
            _logger = log;
        }

        [FunctionName("SAPTestConnectivity")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]      
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("SAPTestConnectivity function triggered.");

            var url = Environment.GetEnvironmentVariable("SapEndpoint", EnvironmentVariableTarget.Process);
            var client = new HttpClientHelper("http://localhost:8080/financial_data", _logger);
            var result = await client.CreateClient();

            return new OkObjectResult(result);
        }
    }
}

