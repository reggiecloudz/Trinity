using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class RoleModification
    {
        [Required]
        public string RoleName { get; set; } = string.Empty;
 
        public string RoleId { get; set; } = string.Empty;
 
        public string[]? AddIds { get; set; }
 
        public string[]? DeleteIds { get; set; }
    }
}