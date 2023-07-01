using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Data.Repository
{
    public interface IConnectionRepository
    {
        // Task SendRequest(string RequesterId, string ReceiverId);
        Task<List<ApplicationUser>> GetUserConnections(string currentUserId);
        Task<IEnumerable<ApplicationUser>> GetUserRecommendations(string currentUserId);
        bool ConnectionExist(string currentUserId, string otherUserId);
        bool ConnectionRequestExist(string currentUserId, string otherUserId);
    }
}