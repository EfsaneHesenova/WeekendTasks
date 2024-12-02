using MediplusMVC.DAL;
using MediplusMVC.Models;
using MediplusMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediplusMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<SliderItem> sliderItems = await _context.SliderItems.OrderByDescending(s=> s.Id).Take(3).ToListAsync();
            IEnumerable<Schedule> schedules = await _context.Schedules.OrderByDescending(s => s.Id).Take(3).ToListAsync();




            HomePageVM homePageVM = new HomePageVM()
            {
                SliderItems = sliderItems,
                Schedules = schedules,
                

            };
            return View(homePageVM);
        }
    }
}
