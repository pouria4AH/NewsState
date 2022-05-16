using Microsoft.AspNetCore.Mvc;

namespace NewsState.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View("AdminIndex");
        }
    }
}
