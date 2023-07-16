using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Trinity.Mvc.Data;
using Trinity.Mvc.Data.Repository;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.ViewComponents
{
    public class SceneInteractionViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SceneInteractionViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long sceneId)
        {
            var likes = await _context.Likes.Where(l => l.ObjectId == sceneId && l.Type == LikableType.Scene).ToListAsync();
            var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var userLikes = new HashSet<string>(likes.Select(u => u.UserId));
            var sceneInteraction = new SceneInteractionModel
            {
                SceneId = sceneId,
                IsLiked = userLikes.Contains(currentUserId),
                Likes = likes.Count()
            };
            return View(sceneInteraction);
        }
    }
}