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
    public class ApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Applicants
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Applicants.Include(a => a.Position).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(a => a.Position)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // GET: Applicants/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,About,PositionId")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                applicant.UserId = HttpContext.User.FindFirst("UserId")!.Value;
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(PositionsController.Information), "Positions", new { id = applicant.PositionId });
            }
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", applicant.PositionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", applicant.UserId);
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Status,About,PositionId,UserId")] Applicant applicant)
        {
            if (id != applicant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.Id))
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
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Id", applicant.PositionId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", applicant.UserId);
            return View(applicant);
        }

        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(a => a.Position)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Applicants == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Applicants'  is null.");
            }
            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant != null)
            {
                _context.Applicants.Remove(applicant);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("/[controller]/{id}/Select")]
        public async Task<IActionResult> Select(long? id)
        {
            if (id == null || _context.Applicants == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .Include(a => a.Position)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (applicant == null)
            {
                return NotFound();
            }

            applicant.Status = "Selected";
            applicant.Position!.PeopleNeeded = applicant.Position.PeopleNeeded - 1;
            applicant.Position.PositionsFilled = applicant.Position.PositionsFilled + 1;
            _context.ProjectSupporters.Add(new ProjectSupporter
            {
                SupporterId = applicant.UserId,
                ProjectId = applicant.Position!.ProjectId,
                Position = applicant.Position.Title,
                ProjectRole = "Staff"
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(PositionsController.Details), "Positions", new { id = applicant.PositionId });
        }

        private bool ApplicantExists(long id)
        {
          return (_context.Applicants?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
