using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Data;

namespace Trinity.Mvc.Controllers
{
    [Route("[controller]")]
    public class StatesController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<StatesController> _logger;

        public StatesController(ILogger<StatesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("{code}/[action]")]
        public IActionResult Cities(string code)
        {
            var state = _context.States.Include(s => s.Cities).ThenInclude(c => c.Projects).FirstOrDefault(s => s.Code == code);
            return View(state);
        }

        [HttpGet("/api/States/{stateId}/Cities")]
        public async Task<JsonResult> GetCities(long stateId)
        {
            var cities = await _context.Cities.Where(c => c.StateId == stateId).OrderBy(c => c.Name).ToListAsync();
            return new JsonResult(cities);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}