using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Controllers
{
    [Route("[controller]")]
    public class DonationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DonationsController> _logger;

        public DonationsController(ILogger<DonationsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,Message,FundraiserId")] Donation donation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}