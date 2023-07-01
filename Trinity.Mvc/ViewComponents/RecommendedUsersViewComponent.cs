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

namespace Trinity.Mvc.ViewComponents
{
    public class RecommendedUsersViewComponent : ViewComponent
    {
        private readonly IConnectionRepository _repo;

        public RecommendedUsersViewComponent(IConnectionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            return View(await _repo.GetUserRecommendations(currentUser));
            // var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            // var users = await _context.Users.Where(u => u.Id != currentUser).ToListAsync();

            // var requests = await _context.ConnectionRequests
            //     .Where(u => u.RequesterId != currentUser)
            //     .ToListAsync();

            // var connections = await _context.Connections
            //     .Where(c => c.ConnectorId != currentUser)
            //     .ToListAsync();

            // var firstFilter = users.ExceptBy(requests.Select(x => x.ReceiverId), x => x.Id);
            // var secondFilter = firstFilter.ExceptBy(requests.Select(x => x.RequesterId), x => x.Id);
            // var thirdFilter = secondFilter.ExceptBy(connections.Select(x => x.ConnectId), x => x.Id);
            // var fourthFilter = thirdFilter.ExceptBy(connections.Select(x => x.ConnectorId), x => x.Id);

            // return View(fourthFilter);
        }
    }
}