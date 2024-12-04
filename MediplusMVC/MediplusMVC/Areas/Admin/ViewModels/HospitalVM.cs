using MediplusMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MediplusMVC.Areas.Admin.ViewModels
{
    public class HospitalVM
    {
        public Hospital Hospital { get; set; }
        public IEnumerable<SelectListItem>? Doctors { get; set; }
        public List<int>? DoctorIds { get; set; }

    }
}
