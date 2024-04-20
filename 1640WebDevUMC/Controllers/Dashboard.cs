using Microsoft.AspNetCore.Mvc;

namespace _1640WebDevUMC.Controllers
{
    public class Dashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
