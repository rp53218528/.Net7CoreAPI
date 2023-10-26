using Learning.API.Data;
using Learning.API.Models.Domain;

namespace Learning.API.Repositories
{
    public class LocalImagerepository : IImagerepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly LerningDbContext dbContext;

        public LocalImagerepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,
            LerningDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<Image> upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", 
                $"{image.FileName}{image.FileExtension}");

            //upload Image to Local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            //Add Image to the Image Table
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;


        }
    }
}
