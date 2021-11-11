using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectShared.Models;
using ProjectShared;

namespace Api
{
    public static class ApiAccess
    {
        private static readonly MongoDatabase db = new MongoDatabase();

        [FunctionName("AddRecords")]
        public static async Task<IActionResult> AddRecords(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var data = JsonConvert.DeserializeObject<PersonModel>(requestBody);

            db.InsertRecord(data);


            return new OkObjectResult(data);
        }
        [FunctionName("GetRecords")]
        public static async Task<IActionResult> GetRecords(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = db.LoadRecord<PersonModel>();
            return new OkObjectResult(data);
        }
    }
}
