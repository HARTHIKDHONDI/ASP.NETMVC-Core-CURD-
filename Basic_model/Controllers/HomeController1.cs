using Microsoft.AspNetCore.Mvc;

namespace Basic_model.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Basic() {
            string name = "Harthik";
            ViewBag.val = name;
            return View();
        }
    }
}
