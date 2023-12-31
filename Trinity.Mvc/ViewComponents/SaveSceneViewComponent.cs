using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.ViewComponents
{
    public class SaveSceneViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new SceneInputModel());
        }
    }
}