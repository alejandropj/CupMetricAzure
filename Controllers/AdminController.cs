using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
