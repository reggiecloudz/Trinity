using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class WorkExperience : Entity
    {
        public long Id { get; set; }

        public string Title { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Responsibilities { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Organization { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
}