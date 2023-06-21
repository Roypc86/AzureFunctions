using GitHubMonitorApp.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;

namespace GitHubMonitorApp
{
    public static class GitHubMonitor
    {
        [FunctionName("GitHubMonitor")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Our GitHub Monitor processed an action.");


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var result = new OkResult();
            try
            {
                var data = JsonConvert.DeserializeObject<Rootobject>(requestBody);
                log.LogInformation(requestBody);
                log.LogInformation("Information about post:" + data.sender.avatar_url);
            }
            catch (Exception ex)
            {
                log.LogInformation($"Error ocurred: {ex.Message}");
                return new InternalServerErrorResult();
            }

            return result;
        }
    }
}