using MediplusMVC.Areas.Admin.ViewModels;
using MediplusMVC.DAL;
using MediplusMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace MediplusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
           IEnumerable<Appointment> appointments = await _context.Appointments.ToListAsync();
            return View(appointments);
        }
        public IActionResult Delete(int id)
        {
            Appointment? appointment = _context.Appointments.Find(id);
            if (appointment==null)
            {
                return NotFound();
            }
            else
            {
                _context.Appointments.Remove(appointment);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            AppointmentVM model = new AppointmentVM()
            {
                Doctors = _context.Doctors.Where(d => d.IsActive && !d.IsDeleted).Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name + " " + d.Surname
                }),
                Patients = _context.Patients.Where(p => !p.IsDeleted).Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name + " " + p.Surname
                })
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if(!ModelState.IsValid)
            {
                AppointmentVM model = new AppointmentVM()
                {
                    Doctors = _context.Doctors.Where(d => d.IsActive && !d.IsDeleted).Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.Name + " " + d.Surname
                    }),
                    Patients = _context.Patients.Where(p => !p.IsDeleted).Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name + " " + p.Surname
                    }),
                    Appointment = appointment
                };
                ModelState.AddModelError(string.Empty, "Something wrong");
                return View(model);
            }

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Appointment? app = _context.Appointments.Find(id);
            if (app == null)
            {
                return NotFound();
            }

            AppointmentVM model = new AppointmentVM()
            {
                Appointment = app,
                Doctors = _context.Doctors.Where(d => d.IsActive && !d.IsDeleted)
                                          .Select(d => new SelectListItem
                                          {
                                              Value = d.Id.ToString(),
                                              Text = d.Name + " " + d.Surname
                                          }).ToList(),
                Patients = _context.Patients.Where(p => !p.IsDeleted)
                                            .Select(p => new SelectListItem
                                            {
                                                Value = p.Id.ToString(),
                                                Text = p.Name + " " + p.Surname
                                            }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, Appointment appointment)
        {
            Appointment? app = _context.Appointments.Find(id);
            if (app == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                AppointmentVM model = new AppointmentVM()
                {
                    Appointment = app,
                    Doctors = _context.Doctors.Where(d => d.IsActive && !d.IsDeleted)
                                              .Select(d => new SelectListItem
                                              {
                                                  Value = d.Id.ToString(),
                                                  Text = d.Name + " " + d.Surname
                                              }).ToList(),
                    Patients = _context.Patients.Where(p => !p.IsDeleted)
                                                .Select(p => new SelectListItem
                                                {
                                                    Value = p.Id.ToString(),
                                                    Text = p.Name + " " + p.Surname
                                                }).ToList()
                };
                return View(model);
            }

            app.AppointmentDate = appointment.AppointmentDate;
            app.DoctorId = appointment.DoctorId;
            app.PatientId = appointment.PatientId;

            _context.Appointments.Update(app);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
