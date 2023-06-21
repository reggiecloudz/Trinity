using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.ViewComponents
{
    public class HomeProjectsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public HomeProjectsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projects = await _context.Projects
                .Include(p => p.Cause)
                .ToListAsync();
            return View(projects);
        }
    }
}