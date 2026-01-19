using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
namespace WebApplication4.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("text")]
        public async Task Text()
        {
            Response.ContentType = "text/plain";
            await Response.WriteAsync("Hello, world!");
        }
        [HttpGet("html")]
        public async Task Html()
        {
            Response.ContentType = "text/html";
            string htmlContent = @"
            <html>
                <head>
                    <meta charset=""utf-8"">
                    <title>Страница HTML</title>
                </head>
                <body>
                    <h1>Заголовок</h1>
                    <p>абзац</p>
                </body>
            </html>";
            await Response.WriteAsync(htmlContent);
        }
        [HttpGet("json")]
        public async Task Json()
        {
            Response.ContentType = "application/json";
            string jsonContent = @"
            Name = ""Алина"",
            Age = 50";
            await Response.WriteAsync(jsonContent);
        }
        [HttpGet("file")]
        public async Task File()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "file.txt");
            await System.IO.File.WriteAllTextAsync(filePath, "привет :3", Encoding.UTF8);
            Response.ContentType = "text/plain";
            await Response.SendFileAsync(filePath);
        }
        [HttpGet("status")]
        public async Task Status()
        {
            Response.StatusCode = 404;
            await Response.WriteAsync("Not Found");
        }
        [HttpGet("cookie")]
        public async Task Cookie()
        {
            Response.ContentType = "text/plain; charset=utf-8";
            Response.Cookies.Append("user", "Answer");
            await Response.WriteAsync("Для user установлено куки со значением Answer", Encoding.UTF8);
        }

    }
}
