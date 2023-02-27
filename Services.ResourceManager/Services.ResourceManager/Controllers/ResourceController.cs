using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using Services.ResourceManager.AccessLayer;
using Services.ResourceManager.DTO;

namespace Services.ResourceManager.Controllers
{
    [Route("")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IStorageManager _storageManager;

        public ResourceController(IStorageManager storageManager)
        {
            _storageManager = storageManager;
        }

        [HttpGet("DownloadFile")]
        public async Task<IActionResult> GetFile(string path)
        {
            string name = Path.GetFileName(path);
            var fileBytes = await _storageManager.GetFileAsync(path);
            if (fileBytes != null && fileBytes.Length > 0)
            {
                return File(fileBytes, "application/octet-stream", name);
            }
            return NotFound();
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFileAsync([FromForm] IFormFile file, [FromQuery] string path)
        {
            if (file == null)
            {
                return BadRequest();
            }

            try
            {
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                await _storageManager.UploadFileAsync(filePath, path);
                return Ok("Success");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("DeleteFile")]
        public async Task DeleteFileAsync([FromBody]string path)
        {
            await _storageManager.DeleteFileAsync(path);
        }

        [HttpPost("RenameFile")]
        public async Task RenameFileAsync([FromBody] RenameRequest renameRequest)
        {
            if(renameRequest.Path != null && renameRequest.NewPath != null)
                await _storageManager.RenameFile(renameRequest.Path, renameRequest.NewPath);
        }
    }
}
