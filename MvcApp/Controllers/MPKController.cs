﻿using Microsoft.AspNetCore.Mvc;

namespace AHMTZDotNetCore.MvcApp.Controllers
{
    public class MPKController : Controller
    {
        public IActionResult Index()
        {
            string Title = "Business Management";
            string Author = "Kathy";
            string Content = "Business";

            ViewData["Title"] = Title;
            ViewData["Author"] = Author;
            ViewData["Content"] = Content;

            ViewBag.Title = Title;
            ViewBag.Author = Author;
            ViewBag.Content = Content;

            TempData["Title"] = Title;
            TempData["Author"] = Author;
            TempData["Content"] = Content;
            return View();
        }
    }
}