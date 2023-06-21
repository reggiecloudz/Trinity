using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class EventAttendee : Entity
    {
        public long EventId { get; set; }

        public virtual Event? Event { get; set; }

        public string AttendeeId { get; set; } = string.Empty;

        public virtual ApplicationUser? Attendee { get; set; }
    }
}