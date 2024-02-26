using Microsoft.AspNetCore.Mvc;

namespace WebDatLich.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
