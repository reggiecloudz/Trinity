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
    [Route("[controller]/[action]")]
    public class EndeavorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        private readonly ILogger<EndeavorsController> _logger;

        public EndeavorsController(ApplicationDbContext context, ILogger<EndeavorsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Route("/[controller]/{slug}")]
        public async Task<ActionResult> Profile(string slug)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Proposal)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Slug == slug);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("/[controller]/{slug}/Expenditures")]
        public async Task<ActionResult> Expenditures(string slug)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Expenditures)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Slug == slug);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("/[controller]/{slug}/Positions")]
        public async Task<ActionResult> Positions(string slug)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Positions)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Slug == slug);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("/[controller]/{slug}/Events")]
        public async Task<ActionResult> Events(string slug)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Proposal)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Slug == slug);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("/[controller]/{slug}/Supporters")]
        public async Task<ActionResult> Supporters(string slug)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Proposal)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Slug == slug);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [Route("/[controller]/{slug}/Media")]
        public async Task<ActionResult> Media(string slug)
        {
            if (slug == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Proposal)
                .Include(p => p.Cause)
                .Include(p => p.Manager)
                .FirstOrDefaultAsync(m => m.Slug == slug);

            if (project == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = project.Id;
            return View(project);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}