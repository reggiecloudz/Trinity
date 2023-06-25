#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<MembersController> _logger;

        public MembersController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            ILogger<MembersController> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [Route("/[controller]/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        [Route("/[controller]/{id}/Projects")]
        public async Task<IActionResult> Projects(string id)
        {
            ApplicationUser user = await _context.Users
                .Include(m => m.Projects)
                    .ThenInclude(p => p.Cause)
                .FirstOrDefaultAsync(u => u.Id == id);
                
            return View(user);
        }

        [Route("/[controller]/{id}/Applications")]
        public async Task<IActionResult> Applications(string id)
        {
            ApplicationUser user = await _context.Users
                .Include(m => m.Applications)
                    .ThenInclude(a => a.Position)
                        .ThenInclude(p => p.Project)
                .FirstOrDefaultAsync(u => u.Id == id);
                
            return View(user);
        }

        [Route("/[controller]/{id}/Connections")]
        public async Task<IActionResult> Connections(string id)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        [Route("/[controller]/{id}/Invitations")]
        public async Task<IActionResult> Invitations(string id)
        {
            ApplicationUser user = await _context.Users
                .Include(u => u.Invitations)
                    .ThenInclude(i => i.Event)
                        .ThenInclude(e => e.Project)
                .FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        [Route("/[controller]/{id}/Discussions")]
        public async Task<IActionResult> Discussions(string id)
        {
            ApplicationUser user = await _context.Users
                .Include(u => u.Discussions)
                    .ThenInclude(d => d.Category)
                .FirstOrDefaultAsync(u => u.Id == id);

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");

            return View(user);
        }

        [Route("/[controller]/{id}/Subscriptions")]
        public async Task<IActionResult> Subscriptions(string id)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}