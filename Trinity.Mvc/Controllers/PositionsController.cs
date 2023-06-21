using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Controllers
{
    [Route("[controller]/[action]")]
    public class PositionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PositionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EPositions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Positions.Include(p => p.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Positions/Details/5
        [Route("/[controller]/{id}/Details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .Include(p => p.Applicants)
                    .ThenInclude(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (position == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.Cause)
                .FirstOrDefaultAsync(p => p.Id == position.ProjectId);

            var model = new PositionDetailsViewModel
            {
                Position = position,
                Project = project
            };

            return View(model);
        }

        [Route("/[controller]/{id}/Information")]
        public async Task<IActionResult> Information(long? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .Include(p => p.Project)
                    .ThenInclude(p => p!.Cause)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (position == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = position.Id;
            return View(position);
        }

        // GET: EPositions/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return View();
        }

        // POST: EPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Details,Closed,PeopleNeeded,ProjectId")] Position position)
        {
            if (ModelState.IsValid)
            {
                position.Slug = position.Title.ToLower().Replace("'", "").Replace(" ", "-");
                _context.Add(position);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProjectsController.Positions), "Projects", new { id = position.ProjectId });
            }
            return View(position);
        }

        // GET: EPositions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", position.ProjectId);
            return View(position);
        }

        // POST: EPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Slug,Title,Details,Closed,PeopleNeeded,PositionsFilled,ProjectId")] Position position)
        {
            if (id != position.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(position);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionExists(position.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", position.ProjectId);
            return View(position);
        }

        // GET: EPositions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Positions == null)
            {
                return NotFound();
            }

            var position = await _context.Positions
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        // POST: EPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Positions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Positions'  is null.");
            }
            var position = await _context.Positions.FindAsync(id);
            if (position != null)
            {
                _context.Positions.Remove(position);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionExists(long id)
        {
          return (_context.Positions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
