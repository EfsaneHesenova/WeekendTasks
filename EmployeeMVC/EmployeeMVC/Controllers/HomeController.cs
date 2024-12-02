using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
