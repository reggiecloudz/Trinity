using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.ViewComponents
{
    public class SaveApplicantViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(new Applicant());
        }
    }
}