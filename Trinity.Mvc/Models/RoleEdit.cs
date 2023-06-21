#nullable disable
using Trinity.Mvc.Domain;
using Microsoft.AspNetCore.Identity;
 
namespace Trinity.Mvc.Models
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}