using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.ViewComponents
{
    public class ChatSidebarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ChatSidebarViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var rooms = new List<ChatRoomModel>();

            var chats = await _context.Chats
                .Include(c => c.Messages)
                .Include(u => u.Users)
                    .ThenInclude(x => x.User)
                .Where(t => t.Users.Any(y => y.UserId == currentUserId))
                .ToListAsync();

            foreach(var item in chats)
            {
                if(item.Type == ChatType.Private)
                {
                    string[] names = item.Name.Split("-");
                    var result = Array.Find(names, element => element != HttpContext.User.FindFirst(ClaimTypes.Name)!.Value);
                    var chatUser = _context.Users.FirstOrDefault(u => u.UserName == result);
                    
                    
                    rooms.Add(new ChatRoomModel
                    {
                        // Id = item.Id,
                        Name = chatUser!.FullName,
                        Photo = $"media/members/{chatUser.ProfileImage}",
                        Chat = item
                    });
                }

                if(item.Type == ChatType.Room)
                {
                    rooms.Add(new ChatRoomModel
                    {
                        // Id = item.Id,
                        Name = item.Name,
                        Photo = "assets/images/groupchat.png",
                        Chat = item
                    });
                }
            }
            
            return View(rooms);
        }
    }
}