using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.ViewComponents
{
    public class ChatBoxViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public ChatBoxViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
    }
}