using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.ViewComponents
{
    public class PostRepliesViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public PostRepliesViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long postId)
        {
            var replies = await _context.Replies
                .Where(r => r.PostId == postId)
                .Include(r => r.Author)
                .Include(r => r.Votes)
                .Include(r => r.Parent)
                .Include(r => r.Replies)
                .ToListAsync();
            return View();
        }
    }
}