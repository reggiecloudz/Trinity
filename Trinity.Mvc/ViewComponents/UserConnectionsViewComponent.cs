using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Trinity.Mvc.Data.Repository;

namespace Trinity.Mvc.ViewComponents
{
    public class UserConnectionsViewComponent : ViewComponent
    {
        private readonly IConnectionRepository _repo;

        public UserConnectionsViewComponent(IConnectionRepository repo)
        {
            _repo = repo;
        }   

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            return View(await _repo.GetUserConnections(currentUser));
        }     
    }
}