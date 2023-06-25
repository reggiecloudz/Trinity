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
using Trinity.Mvc.Infrastructure.Helpers;

namespace Trinity.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class DiscussionGroupsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DiscussionGroupsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: DiscussionGroups
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DiscussionGroups.Include(d => d.Moderator);
            return View(await applicationDbContext.ToListAsync());
        }

        [Route("/[controller]/{id}/Details")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.DiscussionGroups == null)
            {
                return NotFound();
            }

            var discussionGroup = await _context.DiscussionGroups
                .Include(d => d.Moderator)
                .Include(d => d.Category)
                .Include(d => d.Topics)
                .Include(d => d.Posts)
                    .ThenInclude(p => p.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (discussionGroup == null)
            {
                return NotFound();
            }
            ViewData["DiscussionGroupId"] = discussionGroup.Id;
            ViewData["TopicId"] = new SelectList(
                _context.Topics.Where(t => t.DiscussionGroupId == discussionGroup.Id).ToList(), "Id", "Name");
            return View(discussionGroup);
        }

        // GET: DiscussionGroups/Create
        public IActionResult Create()
        {
            ViewData["ModeratorId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: DiscussionGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,CategoryId")] DiscussionGroup discussionGroup)
        {
            if (ModelState.IsValid)
            {
                discussionGroup.Slug = FriendlyUrlHelper.GetFriendlyTitle(discussionGroup.Name);
                discussionGroup.ModeratorId = HttpContext.User.FindFirst("UserId")!.Value;
                _context.Add(discussionGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MembersController.Discussions), "Members", new { id = HttpContext.User.FindFirst("UserId")!.Value });
            }
            
            return RedirectToAction(nameof(MembersController.Discussions), "Members", new { id = HttpContext.User.FindFirst("UserId")!.Value });
        }

        // GET: DiscussionGroups/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.DiscussionGroups == null)
            {
                return NotFound();
            }

            var discussionGroup = await _context.DiscussionGroups.FindAsync(id);
            if (discussionGroup == null)
            {
                return NotFound();
            }
            ViewData["ModeratorId"] = new SelectList(_context.Users, "Id", "Id", discussionGroup.ModeratorId);
            ViewData["TopicId"] = new SelectList(
                _context.Topics.Where(t => t.DiscussionGroupId == discussionGroup.Id).ToList(), "Id", "Name");
            return View(discussionGroup);
        }

        // POST: DiscussionGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Slug,Name,Description,Photo,ModeratorId")] DiscussionGroup discussionGroup)
        {
            if (id != discussionGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussionGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionGroupExists(discussionGroup.Id))
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
            ViewData["ModeratorId"] = new SelectList(_context.Users, "Id", "Id", discussionGroup.ModeratorId);
            return View(discussionGroup);
        }

        // GET: DiscussionGroups/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.DiscussionGroups == null)
            {
                return NotFound();
            }

            var discussionGroup = await _context.DiscussionGroups
                .Include(d => d.Moderator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discussionGroup == null)
            {
                return NotFound();
            }

            return View(discussionGroup);
        }

        // POST: DiscussionGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.DiscussionGroups == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DiscussionGroups'  is null.");
            }
            var discussionGroup = await _context.DiscussionGroups.FindAsync(id);
            if (discussionGroup != null)
            {
                _context.DiscussionGroups.Remove(discussionGroup);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionGroupExists(long id)
        {
          return (_context.DiscussionGroups?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
