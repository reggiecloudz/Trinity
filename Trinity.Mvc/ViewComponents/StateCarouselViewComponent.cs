using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.ViewComponents
{
    public class StateCarouselViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public StateCarouselViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var states = await _context.States.OrderBy(s => s.Name).ToListAsync();
            return View(states);
        }
    }
}