using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        [Route("/[controller]/send-request")]
        [HttpPost]
        public async Task<IActionResult> SendRequest(ConnectionRequestInputModel model)
        {
            var currentUserId = HttpContext.User.FindFirst("UserId")!.Value;
            if (ModelState.IsValid)
            {
                var member = _context.Users.Where(m => m.Id == model.ReceiverId).FirstOrDefault();
                await _context.ConnectionRequests.AddAsync(new ConnectionRequest
                {
                    RequesterId = HttpContext.User.FindFirst("MemberId")!.Value,
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}