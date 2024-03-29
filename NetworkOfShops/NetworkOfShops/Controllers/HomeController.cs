﻿using Microsoft.AspNetCore.Mvc;
using NetworkOfShops.Models;
using System.Diagnostics;

namespace NetworkOfShops.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [ResponseCache(Duration = 120)]
        public IActionResult Index()
        {
            return View();
        }
        [ResponseCache(Duration = 120)]
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