using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ADSeek.Models;
using ADSeek.Services;

namespace ADSeek.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ActiveDirectoryService _adService;

        public HomeController(ILogger<HomeController> logger, ActiveDirectoryService adService)
        {
            _logger = logger;
            _adService = adService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _adService.SearchAsync("OU=Users,DC=OLEG,DC=local", "(objectClass=user)");

            return Json(users.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}