using Microsoft.AspNetCore.Mvc;
using NewsState.Application.Services.interfaces;

namespace NewsState.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlogServices _blogServices;

        public HomeController(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ListTag = await _blogServices.ListTags();
            return View("AdminIndex");
        }
    }
}
