using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Data;
using Trinity.Mvc.Data.Repository;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Controllers
{
    [Route("[controller]")]
    public class ScenesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILikeRepository _likeRepo;
        private readonly ILogger<ScenesController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ScenesController(ILogger<ScenesController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, ILikeRepository likeRepo)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _likeRepo = likeRepo;
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create([Bind("Title,PhotoUpload,Content,JourneyId")] SceneInputModel model)
        {
            if (ModelState.IsValid)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/scenes");
                string photoName = Guid.NewGuid().ToString() + "_" + model.PhotoUpload!.FileName;
                string photoFilePath = Path.Combine(uploadsDir, photoName);
                FileStream fs = new FileStream(photoFilePath, FileMode.Create);
                await model.PhotoUpload.CopyToAsync(fs);
                fs.Close();
                
                var scene = new Scene
                {
                    Title = model.Title,
                    Content = model.Content,
                    Photo = photoName,
                    JourneyId = model.JourneyId
                };
                await _context.AddAsync(scene);
                await _context.SaveChangesAsync();
                var journey = await _context.Journeys.Include(j => j.Project).ThenInclude(p => p!.Manager).FirstOrDefaultAsync(j => j.Id == model.JourneyId);
                return new JsonResult(new SceneResponseModel
                {
                    Title = scene.Title,
                    ScenePhoto = scene.Photo,
                    Content = scene.Content,
                    ProjectName = journey!.Project!.Name,
                    ProjectManager = journey!.Project!.Manager!.FullName,
                    ProjectPhoto =  journey!.Project!.Photo,
                    Message = "Your scene has been added"
                });
            }
            return new JsonResult("Your scene was not saved");
        }

        [Route("{sceneId}/[action]")]
        public async Task<JsonResult> Like(long? sceneId)
        {
            if (sceneId == null)
            {
                return new JsonResult("Scene not found");
            }

            var scene = await _context.Scenes.FirstOrDefaultAsync(s => s.Id == sceneId);

            if (scene == null)
            {
                return new JsonResult("Scene not found");
            }

            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            if (_likeRepo.LikeExist(scene.Id, LikableType.Scene, currentUserId))
            {
                var like = _context.Likes.FirstOrDefault(l => l.ObjectId == scene.Id && l.Type == LikableType.Scene && l.UserId == currentUserId);
                if (like == null) 
                {
                    return new JsonResult("Something went wrong.");
                }
                _context.Likes.Remove(like);
                _context.SaveChanges();
                return new JsonResult("");
            }

            var newLike = new Like
            {
                ObjectId = scene.Id,
                Type = LikableType.Scene,
                UserId = currentUserId
            };
            _context.Likes.Add(newLike);

            return new JsonResult("");
        }
    }
}