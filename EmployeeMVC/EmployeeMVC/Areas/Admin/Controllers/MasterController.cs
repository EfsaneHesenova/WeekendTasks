using EmployeeMVC.Areas.Admin.ViewModels;
using EmployeeMVC.DAL;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Manager")]
    public class MasterController : Controller
    {
        private readonly AppDbContext _context;

        public MasterController(AppDbContext context)
        {
           _context = context;
        }




        public async Task<IActionResult> Index()
        {
            IEnumerable<Master> masters = await _context.Masters.ToListAsync();
            return View(masters);
        }

        public IActionResult Delete(int id)
        {
            Master? master = _context.Masters.Find(id);
            if (master == null)
            {
                return NotFound();
            }
            else
            {
                _context.Masters.Remove(master);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            MasterVM model = new MasterVM()
            {
                Services = _context.Services.Where(s => s.IsActive && !s.IsDeleted).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Title
                })
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Master master)
        {
            if (!ModelState.IsValid)
            {
                MasterVM model = new MasterVM()
                {
                    Services = _context.Services.Where(s => s.IsActive && !s.IsDeleted).Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Title
                    }),
                    Master = master
                };
                ModelState.AddModelError(string.Empty, "Something wrong");
                return View(model);
            }

            _context.Masters.Add(master);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Master? app = _context.Masters.Find(id);
            if (app == null)
            {
                return NotFound();
            }

            MasterVM model = new MasterVM()
            {
                Master = app,
                Services = _context.Services.Where(d => d.IsActive && !d.IsDeleted)
                                          .Select(d => new SelectListItem
                                          {
                                              Value = d.Id.ToString(),
                                              Text = d.Title
                                          }).ToList(),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, Master master)
        {
            Master? app = _context.Masters.Find(id);
            if (app == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                MasterVM model = new MasterVM()
                {
                    Master = app,
                    Services = _context.Services.Where(d => d.IsActive && !d.IsDeleted)
                                              .Select(d => new SelectListItem
                                              {
                                                  Value = d.Id.ToString(),
                                                  Text = d.Title
                                              }).ToList()
                };
                return View(model);
            }

            app.Name = master.Name;
            app.SurName = master.SurName;
            app.Email = master.Email;
            app.PhoneNumber = master.PhoneNumber;
            app.Username = master.Username;
            app.ExperienceYear = master.ExperienceYear;
            app.IsActive = master.IsActive;
            app.ServiceId = master.ServiceId;

            _context.Masters.Update(app);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

