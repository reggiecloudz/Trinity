using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Models
{
    public class InviteeViewModel
    {
        public ApplicationUser? User { get; set; }

        public bool IsInvited { get; set; }
    }
}