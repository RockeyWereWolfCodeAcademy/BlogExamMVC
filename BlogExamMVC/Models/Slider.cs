using System.ComponentModel.DataAnnotations;

namespace BlogExamMVC.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [Required, MaxLength(128)]
        public string Description { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        [Range(0, 5)]
        public byte Rating { get; set; }
    }
}
