using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld/
        public string Index()
        {
            return "Esta é minha ação padrão...";
        }

        // GET: /HelloWorld/Welcome
        // Requires using System.Text.Encodings.Web;
        public string Welcome(string name, int ID, int numTimes)
        {
            return HtmlEncoder.Default.Encode($"Ola {name}, rodamos {numTimes}x e seu ID eh {ID}");
        }
    }
}