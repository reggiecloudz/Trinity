using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.ViewComponents
{
    public class SaveProjectViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public SaveProjectViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(new ProjectInputModel
            {
                Project = new Project(),
                Causes = new SelectList(_context.Causes, "Id", "Name")
            });
        }
        
    }
}