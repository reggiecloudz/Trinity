using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Models
{
    public class EventDetailsViewModel
    {
        public Event? Event { get; set; }
        public Project? Project { get; set; }
    }
}