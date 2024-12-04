using MediplusMVC.Areas.Admin.ViewModels;
using MediplusMVC.DAL;
using MediplusMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Reflection.Metadata.Ecma335;

namespace MediplusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HospitalController : Controller
    {
        private readonly AppDbContext _context;

        public HospitalController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Hospital> hospitals = _context.Hospitals.Include(a => a.HospitalDoctors).ThenInclude(a => a.Doctor).ToList();
            return View(hospitals);
        }

        public IActionResult Detail(int id)
        {
            
            Hospital? hospital = _context.Hospitals.Include(a => a.HospitalDoctors).ThenInclude(a => a.Doctor).FirstOrDefault(m => m.Id == id);
            return View(hospital);
        }

        public IActionResult Delete(int id)
        {
            Hospital? hospital = _context.Hospitals.Find(id);
            if (hospital == null)
            {
                return NotFound();
            }
            else
            {
                _context.Hospitals.Remove(hospital);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            HospitalVM model = new HospitalVM()
            {
                Doctors = _context.Doctors.Where(e => !e.IsDeleted).Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name + " " + x.Surname 
                })
            };

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Create(HospitalVM hospitalVM)
        {
            if (!ModelState.IsValid)
            {
                HospitalVM model = new HospitalVM()
                {
                    Doctors = _context.Doctors.Where(e => !e.IsDeleted).Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name + " " + x.Surname
                    }),
                    Hospital = hospitalVM.Hospital,
                    DoctorIds = hospitalVM.DoctorIds
                };
                return View(model);
            }

            Hospital hospital = new Hospital()
            {
                Title = hospitalVM.Hospital.Title,
                Description = hospitalVM.Hospital.Description
            };

            _context.Hospitals.Add(hospital);
            _context.SaveChanges();

            foreach(int item in hospitalVM.DoctorIds) 
            {
                HospitalDoctor hospitalDoctor = new HospitalDoctor()
                {
                    DoctorId = item,
                    HospitalId = hospital.Id
                };

                _context.HospitalDoctors.Add(hospitalDoctor);
                _context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Hospital? hospital = _context.Hospitals?.Include(d => d.HospitalDoctors).ThenInclude(d => d.Doctor).FirstOrDefault(d => d.Id == id);
            
            if (hospital == null)
            {
                return NotFound();
            }

            HospitalVM model = new HospitalVM()
            {
                Doctors = _context.Doctors.Where(e => !e.IsDeleted).Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name + " " + x.Surname
                }),
                Hospital = hospital,
            };

            return View(model);

        }

        [HttpPost]
        public IActionResult Update(int id, HospitalVM hospitalVM)
        {
            Hospital? uptadedhospital = _context.Hospitals.Find(id);
            if (uptadedhospital == null)
            {
                return View("Error");
            }
           
            if(!ModelState.IsValid)
            {
                HospitalVM model = new HospitalVM()
                {
                    Doctors = _context.Doctors.Where(e => !e.IsDeleted).Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.Name + " " + x.Surname
                    }),
                    Hospital = hospitalVM.Hospital,
                    DoctorIds = hospitalVM.DoctorIds
                };
                return View(model);
            }
            uptadedhospital.Title = hospitalVM.Hospital.Title;
            uptadedhospital.Description = hospitalVM.Hospital.Description;

            _context.Hospitals.Update(uptadedhospital);
            _context.SaveChanges();

            List<HospitalDoctor> hospitalDoctors = _context.HospitalDoctors.Where(x => x.HospitalId == id).ToList();

            foreach(HospitalDoctor item in hospitalDoctors)
            {
                HospitalDoctor? hospitalDoctor = _context.HospitalDoctors.FirstOrDefault(a => a.DoctorId == item.DoctorId);
                foreach(int i in hospitalVM.DoctorIds)
                {
                   
                }
            }

            return RedirectToAction("Index");
        }

    }
}
