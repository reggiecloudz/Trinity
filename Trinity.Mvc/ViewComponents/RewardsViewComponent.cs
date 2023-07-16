using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.ViewComponents
{
    public class RewardsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public RewardsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(long fundraiserId)
        {
            var rewards = await _context.Rewards.Where(r => r.FundraiserId == fundraiserId).ToListAsync();
            return View(rewards);
        }
        
    }
}