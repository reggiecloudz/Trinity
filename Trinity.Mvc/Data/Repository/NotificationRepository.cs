using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Hubs;

namespace Trinity.Mvc.Data.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        public ApplicationDbContext _context { get; }
        private IHubContext<NotificationHub> _hubContext;

        public NotificationRepository(ApplicationDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public void Create(Notification notification, string receiverId)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();

            //TODO: Assign notification to users
            
            var userNotification = new UserNotification();
                userNotification.UserId = receiverId;
                userNotification.NotificationId = notification.Id;

                _context.UserNotifications.Add(userNotification);
                _context.SaveChanges();

            _hubContext.Clients.User(receiverId).SendAsync("displayNotification", notification.Text);
        }

        public List<UserNotification> GetUserNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(u=>u.UserId.Equals(userId) && !u.IsRead)
                .Include(n=>n.Notification)
                .ToList();
        }

        public void ReadNotification(int notificationId, string userId)
        {
            var notification = _context.UserNotifications
                .FirstOrDefault(n=>n.UserId.Equals(userId) && n.NotificationId==notificationId);
            notification!.IsRead = true;
            _context.UserNotifications.Update(notification);
            _context.SaveChanges();
        }
    }
}