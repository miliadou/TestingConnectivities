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
using RestSharp;

namespace TestingConnectivities
{
    public class CRMTestConnectivity
    {
        private readonly ILogger<CRMTestConnectivity> _logger;

        public CRMTestConnectivity(ILogger<CRMTestConnectivity> log)
        {
            _logger = log;
        }

        [FunctionName("CRMTestConnectivity")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("CRMTestConnectivity function triggered.");

            var url = Environment.GetEnvironmentVariable("CRMEndpoint", EnvironmentVariableTarget.Process);
            var client = new HttpClientHelper(url, _logger);
            var result = await client.CreateClient();

            return new OkObjectResult(result);
        }
    }
}

