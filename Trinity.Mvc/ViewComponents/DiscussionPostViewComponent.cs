using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.ViewComponents
{
    public class DiscussionPostViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public DiscussionPostViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long discussionId)
        {
            var posts = await _context.Posts
                .Where(p => p.DiscussionGroupId != discussionId)
                .Include(p => p.Topic)
                .Include(p => p.Replies)
                .ToListAsync();
            return View(posts);
        }
    }
}