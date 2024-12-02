using EmployeeMVC.Areas.Admin.ViewModels;
using EmployeeMVC.DAL;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Service> services = await _context.Services.ToListAsync();
            return View(services);
        }

        public IActionResult Delete(int id)
        {
            Service? service = _context.Services.Find(id);
            if (service == null)
            {
                return NotFound();
            }
            else
            {
                _context.Services.Remove(service);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int? Id)
        {
            Service? service = _context.Services.Find(Id);
            if (service == null)
            {
                return NotFound();
            }
            return View(nameof(Create), service);
        }


        [HttpPost]
        public IActionResult Update(Service service)
        {
            Service? updatedService = _context.Services.AsNoTracking().FirstOrDefault(service => service.Id == service.Id);
            if (updatedService == null)
            {
                return BadRequest();
            }
            _context.Services.Update(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
