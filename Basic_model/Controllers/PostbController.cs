using Microsoft.AspNetCore.Mvc;

namespace Basic_model.Controllers
{
    public class PostbController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
