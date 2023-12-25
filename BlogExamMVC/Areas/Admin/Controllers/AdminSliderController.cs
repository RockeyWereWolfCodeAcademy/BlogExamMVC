using BlogExamMVC.Areas.Admin.ViewModels.AdminSliderVM;
using BlogExamMVC.Contexts;
using BlogExamMVC.Helpers;
using BlogExamMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogExamMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminSliderController : Controller
    {
        BlogDbContext _context;
        public AdminSliderController(BlogDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminSliderCreateVM sliderVM) 
        {
            if (sliderVM.ImageFile != null) 
            {
                if (!sliderVM.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "File must be an image!");
                }
                if (!sliderVM.ImageFile.IsValidSize(1000))
                {
                    ModelState.AddModelError("ImageFile", "Image must be less than 1 mb");
                }
            }
            if (!ModelState.IsValid)
            {
                return View(sliderVM);
            }
            var slider = new Slider
            {
                Description = sliderVM.Description,
                ImgUrl = await sliderVM.ImageFile.SaveImageFileAsync(PathConstants.SliderImagePath),
                Rating = sliderVM.Rating,
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdminHome");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            System.IO.File.Delete(Path.Combine(PathConstants.RootPath, data.ImgUrl));
            _context.Sliders.Remove(data);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdminHome");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            var sliderVM = new AdminSliderUpdateVM
            {
                Description = data.Description,
                Rating = data.Rating,
                ImgUrl = data.ImgUrl,
            };
            return View(sliderVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(AdminSliderUpdateVM sliderVM, int? id)
        {
            if (id == null) return BadRequest();
            var data = await _context.Sliders.FindAsync(id);
            if (data == null) return NotFound();
            if (sliderVM.ImageFile != null)
            {
                if (!sliderVM.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "File must be an image!");
                }
                if (!sliderVM.ImageFile.IsValidSize(1000))
                {
                    ModelState.AddModelError("ImageFile", "Image must be less than 1 mb");
                }
            }
            if (!ModelState.IsValid)
            {
                return View(sliderVM);
            }
            if (sliderVM.ImageFile != null)
            {
                System.IO.File.Delete(Path.Combine(PathConstants.RootPath, data.ImgUrl));
                data.ImgUrl = await sliderVM.ImageFile.SaveImageFileAsync(PathConstants.SliderImagePath);
            }  
            data.Rating = sliderVM.Rating;
            data.Description = sliderVM.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "AdminHome");
        }
    }
}
