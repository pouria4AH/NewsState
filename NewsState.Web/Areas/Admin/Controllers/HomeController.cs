using Microsoft.AspNetCore.Mvc;
using NewsState.Application.Services.interfaces;
using NewsState.DataLayer.Dtos;

namespace NewsState.Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBlogServices _blogServices;

        public HomeController(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.ListTag = await _blogServices.ListTags();
            return View("AdminIndex");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(CreatePostDto post, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var res = await _blogServices.CreatePost(post, image);
            }

            return View("AdminIndex");
        }
    }
}
