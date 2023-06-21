using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.ViewComponents
{
    public class RecommendedUsersViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public RecommendedUsersViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = HttpContext.User?.FindFirst("UserId")!.Value;
            var users = await _context.Users.Where(u => u.Id != currentUser).ToListAsync();
            return View(users);
        }
    }
}