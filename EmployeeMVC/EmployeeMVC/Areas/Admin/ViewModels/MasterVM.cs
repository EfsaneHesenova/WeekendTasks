using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeMVC.Areas.Admin.ViewModels
{
    public class MasterVM
    {
        public IEnumerable<SelectListItem>? Services { get; set; }
        public Master? Master { get; set; }
    }
}
