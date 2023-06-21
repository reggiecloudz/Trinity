using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.ViewComponents
{
    public class ConnectionRequestsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ConnectionRequestsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = HttpContext.User?.FindFirst("UserId")!.Value;
            var requests = await _context.ConnectionRequests
                .Where(u => u.ReceiverId != currentUser)
                .Include(u => u.Requester)
                .ToListAsync();
            return View(requests);
        }
    }
}