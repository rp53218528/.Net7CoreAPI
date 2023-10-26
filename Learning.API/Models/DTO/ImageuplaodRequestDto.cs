using System.ComponentModel.DataAnnotations;

namespace Learning.API.Models.DTO
{
    public class ImageuplaodRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
