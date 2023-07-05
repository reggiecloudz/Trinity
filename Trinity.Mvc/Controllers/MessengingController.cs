using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Trinity.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class MessengingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessengingController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public MessengingController(ILogger<MessengingController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }


        [Route("Chat/{id}")]
        public async Task<IActionResult> Chat(long id)
        {
            var chat = await _context.Chats
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(x => x.Id == id);
           
            return View(chat);
        }

        [Route("Create-Message")]
        [HttpPost]
        public async Task<IActionResult> CreateMessage(long chatId, string content)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var message = new ChatMessage
            {
                ChatId = chatId,
                Content = content,
                UserName = user!.UserName,
                FullName = user.FullName
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return RedirectToAction("Chat", new { id = chatId });
        }

        [Route("Create-Room")]
        [HttpPost]
        public async Task<IActionResult> CreateRoom(string name)
        {
            var chat = new Chat
            {
                Name = name,
                Type = ChatType.Room
            };

            var currentUser = await GetCurrentUserAsync();

            chat.Users.Add(new ChatUser
            {
                UserId = currentUser.Id,
                Role = ChatRole.Admin
            });

            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [Route("Create-Private-Room/{userId}")]
        public async Task<IActionResult> CreatePrivateRoom(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var currentUser = await GetCurrentUserAsync();

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == userId);

            if (user == null)
            {
                return NotFound();
            }
            
            var chat = new Chat
            {
                Name = $"{currentUser.UserName}-{user!.UserName}",
                Type = ChatType.Private
            };

            chat.Users.Add(new ChatUser 
            {
                UserId = currentUser.Id
            });

            chat.Users.Add(new ChatUser
            {
                UserId = user.Id
            });

            _context.Chats.Add(chat);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MembersController.Chats), "Members", new { id = currentUser.Id });
        }

        private async Task<ApplicationUser> GetCurrentUserAsync() => await _userManager.FindByIdAsync(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        private string PrivateRoomDisplay(string roomName)
        {
            string[] names = roomName.Split("-");
            var currentUser = User.FindFirst(ClaimTypes.Name)!.Value;
            var result = Array.Find(names, element => element != currentUser);
            return result!;
        }

        private List<PrivateChatViewModel> ChatMapper(List<Chat> chats)
        {
            var rooms = new List<PrivateChatViewModel>();

            foreach (var item in chats)
            {
                var user = _context.Users.FirstOrDefault(u => u.UserName == PrivateRoomDisplay(item.Name));

                rooms.Add(new PrivateChatViewModel
                {
                    Id = item.Id,
                    Name = user!.UserName,
                    FullName = user.FullName,
                    ProfileImage = user.ProfileImage
                });
            }

            return rooms;
        }
    }
}