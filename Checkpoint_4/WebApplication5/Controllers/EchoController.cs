using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Controllers
{
    [Route("[controller]/[action]")]
    [IgnoreAntiforgeryToken] 
    public class EchoController : Controller
    {
        [HttpGet]
        public async Task Get()
        {
            Response.ContentType = "text/plain";
            await Response.WriteAsync("GET request received");
        }

        [HttpPost]
        public async Task Post()
        {
            Response.ContentType = "text/plain";
            await Response.WriteAsync("POST request received");
        }
        [HttpGet]
        public async Task Headers()
        {
            Response.ContentType = "application/json";
            await Response.WriteAsJsonAsync(Request.Headers);
        }
        [HttpGet]
        public async Task Query()
        {
            var query = Request.Query.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
            Response.ContentType = "application/json";
            await Response.WriteAsJsonAsync(query);
        }
        [HttpPost] 
        public async Task Body()
        {
            using var reader = new System.IO.StreamReader(Request.Body);
            string bodyContent = await reader.ReadToEndAsync();
            Response.ContentType = "text/plain";
            await Response.WriteAsync(bodyContent);
        }
    }
}
