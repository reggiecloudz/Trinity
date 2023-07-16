using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

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
        public async Task<JsonResult> Create(DonationInputModel donation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Donation
                {
                    Amount = donation.Amount,
                    Message = donation.Message,
                    FundraiserId = donation.FundraiserId,
                    DonorId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value
                });
                await _context.SaveChangesAsync();
                return new JsonResult("Thank you for your donation");
                
            }
            return new JsonResult("Something went wrong.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}