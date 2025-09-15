
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PAService.Models.ResidentVMs;
using PAService.Services.Interfaces;
using System.Threading;
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

        public async Task<ActionResult> Index(int accountId = 0, CancellationToken cancellationToken = default)
        {
            ViewBag.AccountId = accountId;
            var residents = accountId > 0
                ? await _residentService.GetResidentsByPersonalAccountId(accountId, cancellationToken)
                : await _residentService.GetAllAsync(withPersonalAccount: true, cancellationToken);
            return View(residents);
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            ResidentVM residentDto,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(residentDto);
            }
            var resident = residentDto.ToEntity();
            await _residentService.CreateAsync(resident, cancellationToken);
            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
