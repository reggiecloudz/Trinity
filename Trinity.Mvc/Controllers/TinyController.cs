using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Controllers
{
    [Route("[controller]/[action]")]
    public class TinyController : Controller
    {
        private readonly ILogger<TinyController> _logger;

        public TinyController(ILogger<TinyController> logger)
        {
            _logger = logger;
        }

        [Route("/[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(TextEditorModel model)
        {
            string encodedContent = HttpUtility.HtmlEncode(model.Content);
            ViewBag.EncodedContent = encodedContent;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}