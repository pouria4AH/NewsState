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
            ViewBag.ListTag = await _blogServices.ListTags();
            if (ModelState.IsValid)
            {
                var res = await _blogServices.CreatePost(post, image);
                switch (res)
                {
                    case CreatePostResult.Error:
                        TempData[ErrorMessage] = "مشکلی پیش امده است دوباره تلاش کنید";
                        break;
                    case CreatePostResult.ImageIsNotValid:
                        TempData[WarningMessage] = "عکس وارد شده معتبر نمی باشد";
                        break;
                    case CreatePostResult.Success:
                        TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                        break;
                }
            }

            return View("AdminIndex", post);
        }

        [HttpGet("add-tag")]
        public IActionResult AddTag()
        {
            return View();
        }

        [HttpPost("add-tag"), ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTag(CreateTagDto tag, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                var res = await _blogServices.CreateTag(tag,image);
                switch (res)
                {
                    case CreateTagResult.Error:
                        TempData[ErrorMessage] = "مشکلی پیش امده است دوباره تلاش کنید";
                        break;
                    case CreateTagResult.ImageIsNotValid:
                        TempData[WarningMessage] = "عکس وارد شده معتبر نمی باشد";
                        break;
                    case CreateTagResult.Success:
                        TempData[SuccessMessage] = "عملیات با موفقیت انجام شد";
                        break;

                }
            }
            //TempData[ErrorMessage] = "مشکلی پیش امده است دوباره تلاش کنید";
            return View(tag);
        }

    }
}
