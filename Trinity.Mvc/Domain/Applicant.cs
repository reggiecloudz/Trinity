using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Domain
{
    public class Applicant : Entity
    {
        public Applicant() {}
        
        public long Id { get; set; }

        [StringRange(AllowedValues = new string[] { "Applied", "Withdrawn", "Selected", "Not Selected" }, ErrorMessage = "Choice unknown.")]
        public string Status { get; set; } = "Applied";

        [DataType(DataType.Text)]
        public string About { get; set; } = string.Empty;

        public long PositionId { get; set; }
        public virtual Position? Position { get; set; }

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
}