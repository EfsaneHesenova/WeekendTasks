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
using EmployeeMVC.Migrations;
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
            List<Service> services = await _context.Services.Include(u => u.ServicePhotos).ToListAsync();
            return View(services);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Service? service = await _context.Services.Include(u => u.ServicePhotos).FirstOrDefaultAsync(u => u.Id == id);
            return View(service);
        }
        public async Task<IActionResult> Delete(int id)
        {
            Service? service = await _context.Services
                .Include(u => u.ServicePhotos)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (service == null)
            {
                return NotFound();
            }

            _context.ServicePhotos.RemoveRange(service.ServicePhotos);

            _context.Services.Remove(service);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Service service)
        {

            _context.Services.Add(service);
            _context.SaveChanges();

            foreach (var Image in service.Images)
            {
				if (!Image.CheckType())
				{
					ModelState.AddModelError("Image", "Only image accepted");
					return View(service);
				}

				if (!Image.CheckSize(5))
				{
					ModelState.AddModelError("Image", "Max 5 mb image accepted");
					return View(service);
				}
				string UploadedImageUrl = Image.Upload(_webHostEnvironment.WebRootPath, Path.Combine(@"\", "download", "ServiceImages"));
				
				ServicePhoto servicePhoto = new ServicePhoto()
                {
                    ImageUrl = UploadedImageUrl,
                    ServiceId = service.Id
                };
                _context.ServicePhotos.Add(servicePhoto);
				_context.SaveChanges();
			}
			return RedirectToAction(nameof(Index));

		}

        public async Task<IActionResult> Update(int? id)
        {
            Service? service = await _context.Services.Include(u => u.ServicePhotos).FirstOrDefaultAsync(u => u.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }


        [HttpPost]
        public async Task<IActionResult> Update(Service service)
        {
            Service? updatedService = await _context.Services.Include(u => u.ServicePhotos).FirstOrDefaultAsync(u => u.Id == service.Id);
            if (updatedService == null)
            {
                return BadRequest();
            }
            _context.Services.Update(service);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeletePhotoById(int id)
        {
            ServicePhoto? servicePhoto = await _context.ServicePhotos.FindAsync(id);

            if (servicePhoto == null)
            {
                return NotFound();
            }

            _context.ServicePhotos.Remove(servicePhoto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Update), new { id = servicePhoto.ServiceId });
        }

    }
}
