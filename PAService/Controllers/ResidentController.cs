using DataLib.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PAService.DTOs;
using PAService.Services.Interfaces;
using System.Threading.Tasks;

namespace PAService.Controllers
{
    public class ResidentController : Controller
    {
        private readonly ILogger<ResidentController> _logger;
        private readonly IResidentService _residentService;
        public ResidentController(
            ILogger<ResidentController> logger,
            IResidentService residentService)
        {
            _logger = logger;
            _residentService = residentService;
        }

        public async Task<ActionResult> Index(int accountId = 0)
        {
            ViewBag.AccountId = accountId;
            var residents = await _residentService
                .GetResidentsByPersonalAccountId(accountId);
            return View(residents);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ResidentDTO residentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(residentDto);
            }
            var resident = residentDto.ToEntity();
            await _residentService.CreateAsync(resident);
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
