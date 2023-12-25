using BlogExamMVC.Areas.Admin.ViewModels.AdminSliderVM;
using BlogExamMVC.Contexts;
using BlogExamMVC.ViewModels.SliderVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogExamMVC.Controllers
{
    public class HomeController : Controller
    {
        BlogDbContext _context;
        public HomeController(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult>  Index()
        {
            var sliderListVm = await _context.Sliders.Select(slider => new SliderListVM
            {
                Description = slider.Description,
                ImgUrl = slider.ImgUrl,
                Rating = slider.Rating,
            }).ToListAsync();
            return View(sliderListVm);
        }
    }
}
