using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimpleSurvey.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Logout()
        {

            return View();
        }

        public IActionResult LoggedOut()
        {

            return View();
        }


        public IActionResult Login()
        {

            return View();
        }

        public IActionResult Redirect()
        {

            return View();
        }
    }
}
