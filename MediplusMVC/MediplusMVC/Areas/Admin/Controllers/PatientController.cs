using MediplusMVC.DAL;
using MediplusMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace MediplusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PatientController : Controller
    {
        private readonly AppDbContext _context;

        public PatientController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Patient> patients = await _context.Patients.ToListAsync();
            return View(patients);
        }


        public IActionResult Delete(int id)
        {
            Patient? patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }
            else
            {
                _context.Patients.Remove(patient);
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
        public IActionResult Create(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Something went wrong");
                return View(patient);
            }
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Patient? patient = _context.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(nameof(Create), patient);
        }
        [HttpPost]
        public IActionResult Update(Patient patient)
        {
            Patient? updatedpatient = _context.Patients.AsNoTracking().FirstOrDefault(x => x.Id == patient.Id);
            if (patient == null)
            {
                return NotFound();
            }
            _context.Patients.Update(patient);
            _context.SaveChanges();
            return View(Index);
        }
    }
}
