﻿//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.AspNetCore.Mvc;

namespace Lugat.Controllers
{
    public  class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()=>
           View();
        
        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (username == "Javohir" && password == "Javohir_1402")
            {
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }
    }
}
