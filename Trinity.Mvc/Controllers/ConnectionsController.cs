using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Data;
using Trinity.Mvc.Data.Repository;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Hubs;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ConnectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationHub> _notificationHub; 
        private readonly ILogger<ConnectionsController> _logger;
        private INotificationRepository _notificationRepository;
        
        public ConnectionsController(
            ILogger<ConnectionsController> logger, 
            ApplicationDbContext context, 
            IHubContext<NotificationHub> notificationHub,
            INotificationRepository notificationRepository)
        {
            _context = context;
            _notificationHub = notificationHub;
            _logger = logger;
            _notificationRepository = notificationRepository;
        }

        [Route("send-request")]
        [HttpPost]
        public async Task<IActionResult> SendRequest(ConnectionRequestInputModel model)
        {
            var currentUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            if (ModelState.IsValid)
            {
                var member = _context.Users.FirstOrDefault(m => m.Id == model.ReceiverId);
                await _context.ConnectionRequests.AddAsync(new ConnectionRequest
                {
                    RequesterId = currentUserId,
                    ReceiverId = member!.Id
                });
                await _context.SaveChangesAsync();

                var notification = new Notification{
                    Text = $"A connection request has been sent by {HttpContext.User.FindFirst("FullName")!.Value}"
                };

                _notificationRepository.Create(notification, model.ReceiverId);
                return RedirectToAction(nameof(MembersController.Connections), "Members", new { id = currentUserId });
            }

            return RedirectToAction(nameof(MembersController.Index), "Members", new { id = currentUserId });
        }

        [Route("{requesterId}/Add")]
        public async Task<IActionResult> AddConnection(string requesterId)
        {
            var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var requestUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == requesterId);

            var request = _context.ConnectionRequests.FirstOrDefault(x => x.ReceiverId == currentUser!.Id && x.RequesterId == requestUser!.Id);

            if (request ==  null)
            {
                return RedirectToAction(nameof(MembersController.Index), "Members", new { id = currentUser!.Id });
            }

            var newConnection = new Connection
            {
                ConnectId = currentUser!.Id,
                ConnectorId = requestUser!.Id
            };

            var newChat = new Chat
            {
                Name = $"{currentUser.UserName}-{requestUser.UserName}",
                Type = ChatType.Private
            };

            _context.Add(newConnection);
            newChat.Users.Add(new ChatUser { UserId = currentUser.Id });
            newChat.Users.Add(new ChatUser { UserId = requestUser.Id });
            _context.Add(newChat);
            _context.Remove(request);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(MembersController.Connections), "Members", new { id = currentUser.Id });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}