using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trinity.Mvc.Data;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Controllers;

[Route("[controller]/[action]")]
public class HomeController : Controller
{
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger, 
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

    [Route("/")]
    public IActionResult Index()
    {
        // if (_signInManager.IsSignedIn(HttpContext.User))
        // {
        //     var user = await _userManager.GetUserAsync(HttpContext.User);
        //     return RedirectToAction(nameof(MembersController.Index), "Members", new { id = user.Id });
        // }
        // return RedirectToAction(nameof(AccountController.Login), "Account");
        return View();
    }

    // [Route("/[action]")]
    // public IActionResult Product()
    // {
    //     return View();
    // }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
