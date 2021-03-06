﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebConsole.Models;
using WebConsole.Models.Common;

namespace WebConsole.Controllers
{
    
    public class HomeController : Controller
    {
        [CountFilter]
        public IActionResult Index()
        {
            return View();
        }

        [LoginFilter]
        public ActionResult Index2()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
