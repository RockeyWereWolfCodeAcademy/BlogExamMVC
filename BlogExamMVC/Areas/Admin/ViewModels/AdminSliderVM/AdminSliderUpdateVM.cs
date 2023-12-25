using System.ComponentModel.DataAnnotations;

namespace BlogExamMVC.Areas.Admin.ViewModels.AdminSliderVM
{
    public class AdminSliderUpdateVM
    {
        [Required, MaxLength(128)]
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
        public byte Rating { get; set; }
    }
}
