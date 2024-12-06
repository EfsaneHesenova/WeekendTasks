using EmployeeMVC.Areas.Admin.ViewModels;
using EmployeeMVC.DAL;
using EmployeeMVC.Models;
using EmployeeMVC.Utilites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeMVC.Utilites;

namespace EmployeeMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Manager")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        IWebHostEnvironment _webHostEnvironment;


        public ServiceController(AppDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHostEnvironment = webHost;
           
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
                return View(service);
            }

            if (service.Image.CheckType())
            {
                ModelState.AddModelError("Image", "Only image accepted");
                return View(service);
            }

            if (service.Image.CheckSize(5))
            {
                ModelState.AddModelError("Image", "Max 5 mb image accepted");
                return View(service);
            }
            string UploadedImageUrl = service.Image.Upload(_webHostEnvironment.WebRootPath + @"\download\ServiceImages\");
            service.MainImageUrl = UploadedImageUrl;
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
