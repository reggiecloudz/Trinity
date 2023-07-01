using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Data.Repository
{
    public class ConnectionRepository : IConnectionRepository
    {
        private readonly ApplicationDbContext _context;

        public ConnectionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ConnectionExist(string currentUserId, string otherUserId)
        {
            var connecting = _context.Connections.FirstOrDefault(x => x.ConnectorId == currentUserId && x.ConnectId == otherUserId);
            var connected = _context.Connections.FirstOrDefault(x => x.ConnectorId == otherUserId && x.ConnectId == currentUserId);

            if (connecting == null && connected == null) return false;

            return true;
        }

        public bool ConnectionRequestExist(string currentUserId, string otherUserId)
        {
            var requested = _context.ConnectionRequests.FirstOrDefault(x => x.RequesterId == currentUserId && x.ReceiverId == otherUserId);
            var received = _context.ConnectionRequests.FirstOrDefault(x => x.RequesterId == otherUserId && x.ReceiverId == currentUserId);

            if (requested == null && received == null) return false;

            return true;
        }

        public async Task<List<ApplicationUser>> GetUserConnections(string currentUserId)
        {
            var users = await _context.Users.Where(x => x.Id != currentUserId).ToListAsync();
            var connectedUsers = new List<ApplicationUser>();

            foreach (var item in users)
            {
                if (ConnectionExist(currentUserId, item.Id))
                {
                    connectedUsers.Add(item);
                }
            }

            return connectedUsers;

        }

        public async Task<IEnumerable<ApplicationUser>> GetUserRecommendations(string currentUserId)
        {
            // get list of users that don't include the current user, don't have a request or connection
            var users = await _context.Users.Where(u => u.Id != currentUserId).ToListAsync();
            var recommendedUsers = new List<ApplicationUser>();

            foreach (var item in users)
            {
                if (!ConnectionExist(currentUserId, item.Id) && !ConnectionRequestExist(currentUserId, item.Id))
                {
                    recommendedUsers.Add(item);
                }
            }

            return recommendedUsers;
        }
    }
}