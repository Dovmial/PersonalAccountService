
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PAService.Models;
using PAService.Models.PersonalAccountVMs;
using PAService.Services.Interfaces;
using System.Diagnostics;
using System.Threading;
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

        public async Task<ActionResult> Index(CancellationToken cancellationToken)
        {
            var accounts = await _paService.GetAsync(
                withResidents: true,
                cancellationToken: cancellationToken);
            return View(accounts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PersonalAccountCreateVM accountVM, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var account = accountVM.ToEntity();
                await _paService.CreateAsync(account, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            _logger.LogError("модель не валидна");
            return View(accountVM);
        }

        public async Task<ActionResult> Delete(int accountId, CancellationToken cancellationToken)
        {
            if(accountId < 1)
                return NotFound();

            await _paService.DeleteAsync(accountId, cancellationToken);
            _logger.LogInformation("Удаление {id} успешно", accountId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Update(
            PersonalAccountUpdateVM accountUpdateVM,
            CancellationToken cancellationToken)
        {
            if (accountUpdateVM.Id < 1)
                return NotFound();

            if (ModelState.IsValid)
            {
                var accountToUpdate = await _paService.GetByIdAsync(accountUpdateVM.Id, cancellationToken);
                accountUpdateVM.ToUpdateEntity(accountToUpdate);
                await _paService.UpdateAsync(accountToUpdate, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            _logger.LogError("модель не валидна");
            return View(accountUpdateVM);
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
