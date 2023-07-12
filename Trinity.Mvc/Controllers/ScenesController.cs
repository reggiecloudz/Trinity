using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Controllers
{
    [Route("[controller]")]
    public class ScenesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ScenesController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ScenesController(ILogger<ScenesController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,Title,PhotoUpload,Content,JourneyId")] SceneInputModel scene)
        {
            if (ModelState.IsValid)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/scenes");
                string photoName = Guid.NewGuid().ToString() + "_" + scene.PhotoUpload!.FileName;
                string photoFilePath = Path.Combine(uploadsDir, photoName);
                FileStream fs = new FileStream(photoFilePath, FileMode.Create);
                await scene.PhotoUpload.CopyToAsync(fs);
                fs.Close();

                _context.Add(new Scene
                {
                    Title = scene.Title,
                    Content = scene.Content,
                    Photo = photoName,
                    JourneyId = scene.JourneyId
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ProjectsController.Journey), "Projects", new { id = scene.ProjectId });
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}