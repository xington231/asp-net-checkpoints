using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [IgnoreAntiforgeryToken]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImageUploadController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm(Name = "upload")] IFormFile upload)
        {
            if (upload == null || upload.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Path.GetFileName(upload.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await upload.CopyToAsync(stream);
            }

            var fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
            return Ok(new
            {
                uploaded = true,
                url = fileUrl
            });
        }
    }
}
