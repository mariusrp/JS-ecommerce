using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Storage.V1;
using Google.Apis.Auth.OAuth2;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : ControllerBase
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName = "myproduct-images"; 

        public ImagesController()
        {
            var credential = GoogleCredential.FromFile("/Users/mariusphillips/downloads/brage-406020-639625572035.json");
            _storageClient = StorageClient.Create(credential);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                var imageObjectName = Guid.NewGuid().ToString(); 
                var imageObjectPath = $"images/{imageObjectName}";

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    await _storageClient.UploadObjectAsync(_bucketName, imageObjectPath, file.ContentType, memoryStream);
                }

                var imageUrl = $"https://storage.googleapis.com/{_bucketName}/{imageObjectPath}";
                return Ok(new { Url = imageUrl });
            }
            catch (Exception ex)
            {
                // In production, consider logging the exception and returning a more generic error message
                return StatusCode(500, ex.Message);
            }
        }
    }
}
