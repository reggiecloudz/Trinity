using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Trinity.Mvc.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public RoomViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var chats = _context.ChatUsers
                .Include(x => x.Chat)
                .Where(x => x.UserId == userId && x.Chat!.Type == ChatType.Room)
                .Select(c => c.Chat)
                .ToList();

            return View(chats);
        }
    }
}
