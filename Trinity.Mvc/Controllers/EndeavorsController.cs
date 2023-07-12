using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.Controllers
{
    [Route("Projects")]
    public class EndeavorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly ILogger<EndeavorsController> _logger;

        public EndeavorsController(ApplicationDbContext context, ILogger<EndeavorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Route("{id}/{slug}")]
        public async Task<ActionResult> Profile(long id, string slug)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.City)
                .Include(p => p.Proposal)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .Include(p => p.Fundraiser)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            ViewData["FundraiserId"] = project.Fundraiser!.Id;
            return View(project);
        }

        [Route("{id}/{slug}/Expenditures")]
        public async Task<ActionResult> Expenditures(string slug, long id)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.City)
                .Include(p => p.Expenditures)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("{id}/{slug}/Positions")]
        public async Task<ActionResult> Positions(string slug, long id)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.City)
                .Include(p => p.Positions)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("{id}/{slug}/Events")]
        public async Task<ActionResult> Events(string slug, long id)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.City)
                .Include(p => p.Proposal)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("{id}/{slug}/Supporters")]
        public async Task<ActionResult> Supporters(string slug, long id)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.City)
                .Include(p => p.Proposal)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("{id}/{slug}/Journey")]
        public async Task<ActionResult> Journey(string slug, long id)
        {
            if (slug == null || _context.Journeys == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.City)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .Include(p => p.Journey)
                    .ThenInclude(p => p!.Scenes)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}