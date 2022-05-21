using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NewsState.Application.Services.interfaces;

namespace NewsState.Web.Controllers
{
    public class HomeController : Controller
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
            ViewBag.shortPost = await _blogServices.GetShortPost();
            ViewBag.olderPost = await _blogServices.GetOlderPosts();

            var lastPost = await _blogServices.GetLastPost();
            return View(lastPost);
        }
        [HttpGet("post/{id}")]
        public async Task<IActionResult> Post(long id)
        {
            var post = await _blogServices.GetPost(id);
            if (post == null) return RedirectToAction("Index");
            return View(post);
        }
    }
}