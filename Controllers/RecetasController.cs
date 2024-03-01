using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class RecetasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
