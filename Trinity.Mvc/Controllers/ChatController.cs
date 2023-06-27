using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Trinity.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _chat;

        public ChatController(IHubContext<ChatHub> chat)
        {
            _chat = chat;    
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinChat(string connectionId, string roomName)
        {
            await _chat.Groups.AddToGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveChat(string connectionId, string roomName)
        {
            await _chat.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(string content, string roomName, int chatId, [FromServices]ApplicationDbContext context)
        {
            var message = new ChatMessage
            {
                ChatId = chatId,
                Content = content,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            };

            context.ChatMessages.Add(message);
            await context.SaveChangesAsync();

            await _chat.Clients.Group(roomName)
                .SendAsync("ReceiveMessage", message);

            return Ok();
        }
    }
}
