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
    public class ChatHeaderViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ChatHeaderViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Chat chat)
        {
            var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var model = new ChatRoomModel { Chat = chat };

            if(chat.Type == ChatType.Private)
            {
                var chatUser = await _context.ChatUsers
                    .Where(c => c.UserId != currentUserId)
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.ChatId == chat.Id);

                model.Name = chatUser!.User!.FullName;
                model.Photo = $"media/members/{chatUser.User.ProfileImage}";
                // model.Chat = chat;
            }
            else
            {
                model.Name = chat.Name;
                model.Photo = "assets/images/groupchat.png";
            }

            return View(model);
        }
        
    }
}