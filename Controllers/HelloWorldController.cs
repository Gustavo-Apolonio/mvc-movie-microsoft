using Microsoft.AspNetCore.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            return View();
        }

        // GET: /HelloWorld/Welcome
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            if (name == string.Empty || name == null) name = "Mundo";
            else name = name.Trim();

            ViewData["Message"] = $"Olá {name}!";
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}