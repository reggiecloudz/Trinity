using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Domain
{
    public class Event : Entity
    {
        public Event() {}
        
        public long Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Details { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool IsPublic { get; set; }

        public long ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public virtual ICollection<EventAttendee> Attendees { get; set; } = new List<EventAttendee>();
        public virtual ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();

    }
}