﻿using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            ViewBag.ListTag = await _blogServices.ListTags();
            return View();
        }
    }
}