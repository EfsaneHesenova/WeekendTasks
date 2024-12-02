using MediplusMVC.DAL;
using MediplusMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediplusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderItemController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public SliderItemController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<SliderItem> sliderItems = await _appDbContext.SliderItems.ToListAsync();
            return View(sliderItems);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SliderItem sliderItem)
        {

            if (!ModelState.IsValid)
            {
                return View(sliderItem);
            }

            _appDbContext.SliderItems.Add(sliderItem);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int Id)
        {
            SliderItem? sliderItem = _appDbContext.SliderItems.Find(Id);
            if (sliderItem == null) { return NotFound(); }
            else
            {
                _appDbContext.SliderItems.Remove(sliderItem);
                _appDbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? Id)
        {
            SliderItem? sliderItem = _appDbContext.SliderItems.Find(Id);
            if (sliderItem is null)
            {
                return NotFound("SliderItem not found");
            }
            return View(nameof(Create), sliderItem);
        }

        [HttpPost]
        public IActionResult Update(SliderItem sliderItem)
        {
            SliderItem? updatedsliderItem = _appDbContext.SliderItems.AsNoTracking()
                .FirstOrDefault(x => x.Id == sliderItem.Id);

            if (updatedsliderItem is null)
            {
                return NotFound("SliderItem not found");
            }

            _appDbContext.SliderItems.Update(sliderItem);
            _appDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}

