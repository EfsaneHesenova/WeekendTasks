using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
