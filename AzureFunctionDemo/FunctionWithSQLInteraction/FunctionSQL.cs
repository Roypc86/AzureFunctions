using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureSQL.ToDo;
using AzureSQL.Customer;
using System.Collections.Generic;
using System.Data;

namespace FunctionWithSQLInteraction
{
    public static class FunctionSQL
    {
        [FunctionName("HttpExample")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [Sql(commandText: "dbo.ToDo", connectionStringSetting: "SqlConnectionString")] IAsyncCollector<ToDoItem> toDoItems,
            [Sql("select * from [SalesLT].[Customer]", connectionStringSetting: "SqlConnectionString", commandType: CommandType.Text)] IEnumerable<Customer> customersItems,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            if (!string.IsNullOrEmpty(name))
            {
                // Add a JSON document to the output container.
                var item = new ToDoItem
                {
                    // create a random ID
                    Id = Guid.NewGuid(),
                    title = name,
                    completed = false,
                    url = ""
                };
                await toDoItems.AddAsync(item).ConfigureAwait(false);
            }

            foreach (var item in customersItems) { 
                Console.WriteLine(item.FirstName);
            }

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}
