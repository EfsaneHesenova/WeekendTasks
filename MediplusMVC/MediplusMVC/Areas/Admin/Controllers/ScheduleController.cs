using MediplusMVC.DAL;
using MediplusMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediplusMVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ScheduleController : Controller
	{
		private readonly AppDbContext _appDbContext;

		public ScheduleController(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<IActionResult> Index()
		{
			IEnumerable<Schedule> schedules = await _appDbContext.Schedules.ToListAsync();
			return View(schedules);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Schedule schedule)
		{

			if (!ModelState.IsValid)
			{
				return View(schedule);
			}

			_appDbContext.Schedules.Add(schedule);
			_appDbContext.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Delete(int Id)
		{
			Schedule? schedule = _appDbContext.Schedules.Find(Id);
			if (schedule == null) { return NotFound(); }
			else
			{
				_appDbContext.Schedules.Remove(schedule);
				_appDbContext.SaveChanges();
			}
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Update(int? Id)
		{
			Schedule? schedule = _appDbContext.Schedules.Find(Id);
			if (schedule is null)
			{
				return NotFound("SliderItem not found");
			}
			return View(nameof(Create), schedule);
		}

		[HttpPost]
		public IActionResult Update(Schedule schedule)
		{
			Schedule? updatedschedule = _appDbContext.Schedules.AsNoTracking()
				.FirstOrDefault(x => x.Id == schedule.Id);

			if (updatedschedule is null)
			{
				return NotFound("SliderItem not found");
			}

			_appDbContext.Schedules.Update(schedule);
			_appDbContext.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
	}
}
