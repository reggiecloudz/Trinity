using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Data;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Controllers
{
    [Route("[controller]")]
    public class DiscussionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DiscussionsController> _logger;

        public DiscussionsController(ApplicationDbContext context, ILogger<DiscussionsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> Details(string slug)
        {
            var discussion = await _context.DiscussionGroups
                .Include(d => d.Category)
                .Include(d => d.Moderator)
                .Include(d => d.Topics)
                .Include(d => d.Subscribers)
                .FirstOrDefaultAsync(s => s.Slug == slug);

            var posts = await _context.Posts
                .Where(p => p.DiscussionGroupId == discussion!.Id)
                .Include(p => p.Author)
                .Include(p => p.Likes)
                .Include(p => p.Topic)
                .Include(p => p.Replies)
                .ToListAsync();

            var model = new DiscussionFeedViewModel
            {
                DiscussionGroup = discussion,
                Posts = posts
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}