#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Data;
using Trinity.Mvc.Data.Repository;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConnectionRepository _connectionRepository;
        private readonly IEventRepository _eventRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<MembersController> _logger;

        public MembersController(
            ApplicationDbContext context,
            IConnectionRepository connectionRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            ILogger<MembersController> logger,
            IEventRepository eventRepository)
        {
            _context = context;
            _connectionRepository = connectionRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _eventRepository = eventRepository;
        }

        [Route("/[controller]/{id}")]
        public async Task<IActionResult> Index(string id)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        [Route("/[controller]/{id}/Projects")]
        public async Task<IActionResult> Projects(string id)
        {
            ApplicationUser user = await _context.Users
                .Include(m => m.Projects)
                    .ThenInclude(p => p.Cause)
                .FirstOrDefaultAsync(u => u.Id == id);
                
            return View(user);
        }

        [Route("/[controller]/{id}/Applications")]
        public async Task<IActionResult> Applications(string id)
        {
            ApplicationUser user = await _context.Users
                .Include(m => m.Applications)
                    .ThenInclude(a => a.Position)
                        .ThenInclude(p => p.Project)
                .FirstOrDefaultAsync(u => u.Id == id);
                
            return View(user);
        }

        [Route("/[controller]/{id}/Calendar")]
        public async Task<IActionResult> Calendar(string id)
        {
            ApplicationUser user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
            
            List<EventViewModel> events = _eventRepository.GetUserEvents(user.Id);

            ViewData["Events"] = _eventRepository.EventsJsonSerializer(events);   
            return View(user);
        }

        [Route("/[controller]/{id}/Connections")]
        public async Task<IActionResult> Connections(string id)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        [Route("/[controller]/{id}/Invitations")]
        public async Task<IActionResult> Invitations(string id)
        {
            ApplicationUser user = await _context.Users
                .Include(u => u.Invitations)
                    .ThenInclude(i => i.Event)
                        .ThenInclude(e => e.Project)
                .FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }

        [Route("/[controller]/{id}/Chats")]
        public async Task<IActionResult> Chats(string id)
        {
            ApplicationUser user = await _context.Users
                .Include(u => u.Chats)
                .FirstOrDefaultAsync(u => u.Id == id);
            var connections = await _connectionRepository.GetUserConnections(user.Id);
            var checkboxes = new List<SelectableConnection>();
            foreach (var item in connections)
            {
                checkboxes.Add(new SelectableConnection
                {
                    Value = item.Id,
                    Text = item.FullName,
                    UserPhoto = $"media/members/{item.ProfileImage}",
                    IsChecked = false
                });
            }
            ViewData["Connections"] = checkboxes;
            return View(user);
        }

        [Route("/[controller]/{id}/Discussions")]
        public async Task<IActionResult> Discussions(string id)
        {
            ApplicationUser user = await _context.Users
                .Include(u => u.Discussions)
                    .ThenInclude(d => d.Category)
                .FirstOrDefaultAsync(u => u.Id == id);

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");

            return View(user);
        }

        [Route("/[controller]/{id}/Subscriptions")]
        public async Task<IActionResult> Subscriptions(string id)
        {
            ApplicationUser user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        private async Task GetConnections(Chat chat)
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var connections = await _connectionRepository.GetUserConnections(currentUserId);
            var roomMembers = new HashSet<string>(chat.Users.Select(u => u.UserId));
            var connectionList = new List<UserConnection>();

            foreach (var item in connections)
            {
                connectionList.Add(new UserConnection
                {
                    UserId = item.Id,
                    Name = item.FullName,
                    Selected = roomMembers.Contains(item.Id)
                });
            }

            ViewData["Connections"] = connectionList;
        }
    }
}