using CupMetric.Models;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            return View();
        }        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User usuario)
        {
            return View();
        }
    }
}
