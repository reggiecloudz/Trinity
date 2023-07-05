using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.ViewComponents
{
    public class ChatHeaderViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ChatHeaderViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long chatId)
        {
            var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var chatUser = await _context.ChatUsers
                .Where(c => c.UserId != currentUserId)
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.ChatId == chatId);

            return View(chatUser);
        }
        
    }
}