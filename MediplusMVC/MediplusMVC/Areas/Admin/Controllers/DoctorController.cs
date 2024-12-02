using MediplusMVC.DAL;
using MediplusMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace MediplusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;

        public DoctorController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Doctor> doctors = await _context.Doctors.ToListAsync();
            return View(doctors);
        }



        public IActionResult Delete(int id)
        {
            Doctor? deleteddoctor = _context.Doctors.Find(id);
            if (deleteddoctor == null)
            {
                return NotFound();
            }
            else
            {
                _context.Doctors.Remove(deleteddoctor);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
           if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View(doctor);
            }
            doctor.Username = doctor.Name + doctor.Finkod;
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Doctor? doctor = _context.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(nameof(Create), doctor);
        }
            [HttpPost]
        public IActionResult Update(Doctor doctor)
        {
            Doctor? updateddoctor = _context.Doctors.AsNoTracking().FirstOrDefault(x=>x.Id==doctor.Id);
            if (doctor == null)
            {
                return NotFound();
            }
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
            return View(Index);
        }
        
    }
}
