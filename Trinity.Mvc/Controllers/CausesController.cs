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
    public class CausesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CausesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Causes
        [Route("/[controller]")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Causes.Include(c => c.Parent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Causes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }

            var cause = await _context.Causes
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cause == null)
            {
                return NotFound();
            }

            return View(cause);
        }

        // GET: Causes/Create
        public IActionResult Create()
        {
            ViewData["ParentId"] = new SelectList(_context.Causes, "Id", "Name");
            return View();
        }

        // POST: Causes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ParentId")] Cause cause)
        {
            if (cause.ParentId == 0)
            {
                cause.ParentId = null;
            }
            if (ModelState.IsValid)
            {
                cause.Slug = cause.Name.ToLower().Replace("'", "").Replace(" ", "-");
                _context.Add(cause);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParentId"] = new SelectList(_context.Causes, "Id", "Id", cause.ParentId);
            return View(cause);
        }

        // GET: Causes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }

            var cause = await _context.Causes.FindAsync(id);
            if (cause == null)
            {
                return NotFound();
            }
            ViewData["ParentId"] = new SelectList(_context.Causes, "Id", "Id", cause.ParentId);
            return View(cause);
        }

        // POST: Causes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Slug,Name,ParentId")] Cause cause)
        {
            if (id != cause.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cause);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CauseExists(cause.Id))
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
            ViewData["ParentId"] = new SelectList(_context.Causes, "Id", "Id", cause.ParentId);
            return View(cause);
        }

        // GET: Causes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Causes == null)
            {
                return NotFound();
            }

            var cause = await _context.Causes
                .Include(c => c.Parent)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cause == null)
            {
                return NotFound();
            }

            return View(cause);
        }

        // POST: Causes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Causes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Causes'  is null.");
            }
            var cause = await _context.Causes.FindAsync(id);
            if (cause != null)
            {
                _context.Causes.Remove(cause);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CauseExists(long id)
        {
          return (_context.Causes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
