using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.ViewComponents
{
    public class AuthorPostsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public AuthorPostsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string authorId)
        {
            var posts = await _context.Posts
                .Where(p => p.AuthorId == authorId)
                .Include(p => p.Author)
                .Include(p => p.Likes)
                .Include(p => p.Replies)
                .ToListAsync();
            return View(posts);
        }
    }
}