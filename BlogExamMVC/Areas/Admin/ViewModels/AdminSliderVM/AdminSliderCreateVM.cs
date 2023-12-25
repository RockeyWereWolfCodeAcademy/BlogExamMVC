using System.ComponentModel.DataAnnotations;

namespace BlogExamMVC.Areas.Admin.ViewModels.AdminSliderVM
{
    public class AdminSliderCreateVM
    {
        [Required, MaxLength(128)]
        public string Description { get; set; }
        [Required]
        public IFormFile ImageFile { get; set; }
        [Range(0, 5)]
        public byte Rating { get; set; }
    }
}
