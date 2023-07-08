using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Models
{
    public class ChatRoomInputModel
    {
        public string Name { get; set; } = string.Empty;
        public List<ApplicationUser> Connections { get; set; } = new List<ApplicationUser>();
    }
}