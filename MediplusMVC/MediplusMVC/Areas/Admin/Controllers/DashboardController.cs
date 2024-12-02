using MediplusMVC.DAL;
using MediplusMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediplusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}
