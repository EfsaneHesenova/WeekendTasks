using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeMVC.Areas.Admin.ViewModels
{
    public class OrderVM
    {
        public IEnumerable<SelectListItem>? Services { get; set; }
        public IEnumerable<SelectListItem>? Masters { get; set; }
        public Order? Order { get; set; }
        
    }
}
