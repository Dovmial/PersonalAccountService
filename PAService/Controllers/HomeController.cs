
using DataLib.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PAService.Models;
using PAService.Services.Implemantations;
using PAService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PAService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPersonalAccountService _paService;

        public HomeController(
            ILogger<HomeController> logger,
            IPersonalAccountService paService)
        {
            _logger = logger;
            _paService = paService;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _paService.Get());
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PersonalAccount account)
        {
            if (ModelState.IsValid)
            {
                await _paService.CreateAsync(account);
                return RedirectToAction(nameof(Index));
            }
            return View(account);
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
