using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.ViewComponents
{
    public class SaveEventViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SaveEventViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(new Event());
        }
    }
}