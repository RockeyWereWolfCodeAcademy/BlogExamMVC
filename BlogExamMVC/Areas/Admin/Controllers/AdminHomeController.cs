using BlogExamMVC.Areas.Admin.ViewModels.AdminSliderVM;
using BlogExamMVC.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogExamMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        BlogDbContext _context;
        public AdminHomeController(BlogDbContext context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var sliderListVm = await _context.Sliders.Select(slider => new AdminSliderListVM
            {
                Id = slider.Id,
                Description = slider.Description,
                ImgUrl = slider.ImgUrl,
                Rating = slider.Rating,
            }).ToListAsync();
            return View(sliderListVm);
        }
    }
}
