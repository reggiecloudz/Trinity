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
    public class UserChatsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IChatRepository _repo;
        
        public UserChatsViewComponent(ApplicationDbContext context, IChatRepository repo)
        {
            _context = context;
            _repo = repo;
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
                    var result = Array.Find(names, element => element != currentUserId);
                    var chatUser = _context.Users.FirstOrDefault(u => u.UserName == result);
                    
                    rooms.Add(new ChatRoomModel
                    {
                        // Id = item.Id,
                        Name = chatUser!.FullName,
                        Photo = $"media/members/{chatUser.ProfileImage}",
                        Chat = item,
                        ChatRole = _repo.GetChatUserRole(item, chatUser.Id)
                    });
                }

                if(item.Type == ChatType.Room)
                {
                    rooms.Add(new ChatRoomModel
                    {
                        // Id = item.Id,
                        Name = item.Name,
                        Photo = "assets/images/groupchat.png",
                        Chat = item,
                        ChatRole = _repo.GetChatUserRole(item, currentUserId)
                    });
                }
            }
            
            return View(rooms);
        }
    }
}