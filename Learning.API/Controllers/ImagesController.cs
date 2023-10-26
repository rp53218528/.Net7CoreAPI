using Learning.API.Models.Domain;
using Learning.API.Models.DTO;
using Learning.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Learning.API.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagerepository imagerepository;

        public ImagesController(IImagerepository imagerepository)
        {
            this.imagerepository = imagerepository;
        }
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageuplaodRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                //convert DTO to domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };
                //user repository to upload image 
                await imagerepository.upload(imageDomainModel);
                return Ok(imageDomainModel);

            }
            return BadRequest(ModelState); 
        }
        private void ValidateFileUpload(ImageuplaodRequestDto request)
        {
            var allowedExtentensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtentensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Extension");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "file size more then 10MB, please upload a smaller size file.");
            }
        }



    }
}
