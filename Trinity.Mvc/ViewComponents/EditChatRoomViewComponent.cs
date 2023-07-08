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
    public class EditChatRoomViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly IChatRepository _repo;

        public EditChatRoomViewComponent(ApplicationDbContext context, IChatRepository repo)
        {
            _context = context;
            _repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync(long chatId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == chatId);
            var model = new ChatRoomUpdateModel
            {
                ChatId = chat!.Id,
                Name = chat.Name,
                Connections = await _repo.GetSelectableUsers(chat, HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
            };

            return View(model);
        }
        
    }
}