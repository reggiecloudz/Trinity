using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Trinity.Mvc.Controllers
{
    [Route("[controller]/[action]")]
    public class EditorController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnviornment;

        public EditorController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnviornment = webHostEnvironment;
        }

        public IActionResult TinyMceUpload(IFormFile file)
        {
            var location = UploadImageToServer(file);
            return Content(location);
        }

        public string UploadImageToServer(IFormFile file)
        {
            var uniqueFileName = "";
            var fullFilePath = "";
            if (file != null)
            {
                var uploadfilepath = Path.Combine(_webHostEnviornment.WebRootPath, "media/posts");
                uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                fullFilePath = Path.Combine(uploadfilepath, uniqueFileName);
                file.CopyTo(new FileStream(fullFilePath, FileMode.Create));
            }
            return "/media/posts/" + uniqueFileName;
        }
    }
}
