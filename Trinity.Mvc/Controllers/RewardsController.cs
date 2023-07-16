using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Controllers
{
    [Route("Rewards")]
    public class RewardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RewardsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(Reward reward)
        {
            if (ModelState.IsValid)
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/rewards");
                string photoName = Guid.NewGuid().ToString() + "_" + reward.PhotoUpload!.FileName;
                string photoFilePath = Path.Combine(uploadsDir, photoName);
                FileStream fs = new FileStream(photoFilePath, FileMode.Create);
                await reward.PhotoUpload.CopyToAsync(fs);
                fs.Close();

                reward.Photo = photoName;
                await _context.AddAsync(reward);
                await _context.SaveChangesAsync();

                return new JsonResult(new RewardResponseModel
                {
                    Id = reward.Id,
                    Item = reward.Item,
                    Quantity = reward.Quantity,
                    AmountNeeded = reward.AmountNeeded,
                    Message = "You reward has been added!"
                });
            }
            return new JsonResult("Reward not saved");
        }
    }
}