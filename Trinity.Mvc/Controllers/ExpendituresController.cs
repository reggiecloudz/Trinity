using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Controllers
{
    [Route("[controller]/[action]")]
    public class ExpendituresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExpendituresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Expenditures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Expenditures.Include(e => e.Project);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Expenditures/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Expenditures == null)
            {
                return NotFound();
            }

            var expenditure = await _context.Expenditures
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenditure == null)
            {
                return NotFound();
            }

            return View(expenditure);
        }

        // GET: Expenditures/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id");
            return View();
        }

        // POST: Expenditures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Item,Purpose,Cost,ProjectId")] Expenditure expenditure)
        {
            if (ModelState.IsValid)
            {
                expenditure.Slug = expenditure.Item.ToLower().Replace("'", "").Replace(" ", "-");
                _context.Add(expenditure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProjectsController.Expenditures), "Projects", new { id = expenditure.ProjectId });
            }
            return View(expenditure);
        }

        // GET: Expenditures/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Expenditures == null)
            {
                return NotFound();
            }

            var expenditure = await _context.Expenditures.FindAsync(id);
            if (expenditure == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", expenditure.ProjectId);
            return View(expenditure);
        }

        // POST: Expenditures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Slug,Item,Purpose,Cost,ProjectId")] Expenditure expenditure)
        {
            if (id != expenditure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expenditure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenditureExists(expenditure.Id))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", expenditure.ProjectId);
            return View(expenditure);
        }

        // GET: Expenditures/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Expenditures == null)
            {
                return NotFound();
            }

            var expenditure = await _context.Expenditures
                .Include(e => e.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expenditure == null)
            {
                return NotFound();
            }

            return View(expenditure);
        }

        // POST: Expenditures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Expenditures == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Expenditures'  is null.");
            }
            var expenditure = await _context.Expenditures.FindAsync(id);
            if (expenditure != null)
            {
                _context.Expenditures.Remove(expenditure);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpenditureExists(long id)
        {
          return (_context.Expenditures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
