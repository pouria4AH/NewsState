using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace NewsState.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}