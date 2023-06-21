using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class InvitationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvitationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invitations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invitations.Include(i => i.Event).Include(i => i.Member);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Invitations/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Invitations == null)
            {
                return NotFound();
            }

            var invitation = await _context.Invitations
                .Include(i => i.Event)
                .Include(i => i.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitation == null)
            {
                return NotFound();
            }

            return View(invitation);
        }

        // GET: Invitations/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id");
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Invitations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Message,EventId,MemberId")] Invitation invitation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invitation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EventsController.Details), "Events", new { id = invitation.EventId });
            }
            return View(invitation);
        }

        // GET: Invitations/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Invitations == null)
            {
                return NotFound();
            }

            var invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", invitation.EventId);
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Id", invitation.MemberId);
            return View(invitation);
        }

        // POST: Invitations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Acceptance,Message,EventId,MemberId")] Invitation invitation)
        {
            if (id != invitation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invitation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvitationExists(invitation.Id))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Id", invitation.EventId);
            ViewData["MemberId"] = new SelectList(_context.Users, "Id", "Id", invitation.MemberId);
            return View(invitation);
        }

        // GET: Invitations/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Invitations == null)
            {
                return NotFound();
            }

            var invitation = await _context.Invitations
                .Include(i => i.Event)
                .Include(i => i.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invitation == null)
            {
                return NotFound();
            }

            return View(invitation);
        }

        // POST: Invitations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Invitations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Invitations'  is null.");
            }
            var invitation = await _context.Invitations.FindAsync(id);
            if (invitation != null)
            {
                _context.Invitations.Remove(invitation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Route("/[controller]/{id}/Accept")]
        public async Task<IActionResult> Accept(long? id)
        {
            if (id == null || _context.Invitations == null)
            {
                return NotFound();
            }

            var invite = await _context.Invitations.FirstOrDefaultAsync(i => i.Id == id);
            
            if (invite == null)
            {
                return NotFound();
            }

            if (_context.EventAttendees.Any(e => e.AttendeeId == invite.MemberId && e.EventId == invite.EventId))
            {
                return RedirectToAction(nameof(MembersController.Invitations), "Members", new { id = HttpContext.User.FindFirst("UserId")!.Value });
            }

            var ea = _context.EventAttendees.Add(new EventAttendee
            {
                EventId = invite.EventId,
                AttendeeId = HttpContext.User.FindFirst("UserId")!.Value
            });
            
            invite.Acceptance = "Accepted";

            _context.SaveChanges();

            return RedirectToAction(nameof(MembersController.Invitations), "Members", new { id = HttpContext.User.FindFirst("UserId")!.Value });
        }

        private bool InvitationExists(long id)
        {
          return (_context.Invitations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
