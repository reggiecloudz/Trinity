using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.ViewComponents
{
    public class SendInvitationViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        
        public SendInvitationViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long projectId, long eventId)
        {
            // get project supporters
            var supporters = await _context.ProjectSupporters
                .Where(ps => ps.ProjectId == projectId && ps.SupporterId != HttpContext.User.FindFirst("UserId")!.Value)
                .Include(ps => ps.Supporter)
                .ToListAsync();

            // get event invitations
            var invitations = await _context.Invitations
                .Where(i => i.EventId == eventId)
                .ToListAsync();

            if (invitations.Any())
            {
                var results = supporters.ExceptBy(invitations.Select(x => x.MemberId), x => x.SupporterId);
                return View(results);
            }
            
            return View(supporters);
        }
    }
}