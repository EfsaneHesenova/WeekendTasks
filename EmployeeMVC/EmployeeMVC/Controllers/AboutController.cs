using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
