using Microsoft.AspNetCore.Mvc;

namespace Lugat.Controllers
{
    public  class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()=>
           View();
        
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (username == "Javohir" && password == "Javohir")
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }
    }
}
