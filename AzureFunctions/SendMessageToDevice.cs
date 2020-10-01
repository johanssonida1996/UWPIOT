using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Devices;
using SharedLibariesCore.Models;
using SharedLibariesCore.Services;

namespace AzureFunctions
{
    public static class SendMessageToDevice
    {
        private static readonly ServiceClient serviceClient =
            ServiceClient.CreateFromConnectionString(Environment.GetEnvironmentVariable("IotHubConnection"));

        [FunctionName("SendMessageToDevice")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            
            string targetDeviceId = req.Query["targetdeviceid"];
            string message = req.Query["message"];

            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();


            var data = JsonConvert.DeserializeObject<BodyMessageModel>(requestBody);


            targetDeviceId = targetDeviceId ?? data?.TargetDeviceId;
            message = message ?? data?.Message;


            await DeviceServices.SendMessageToDeviceAsync(serviceClient, targetDeviceId, message);


            return new OkResult();
        }
    }
}
