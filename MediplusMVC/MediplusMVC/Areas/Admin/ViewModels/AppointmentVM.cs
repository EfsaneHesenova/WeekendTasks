using MediplusMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediplusMVC.Areas.Admin.ViewModels
{
    public class AppointmentVM
    {
        public IEnumerable<SelectListItem>? Doctors { get; set; } 
        public IEnumerable<SelectListItem>? Patients { get; set; }
        public Appointment? Appointment { get; set; }
    }
}
