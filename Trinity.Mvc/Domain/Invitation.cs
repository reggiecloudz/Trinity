using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Domain
{
    public class Invitation : Entity
    {
        public long Id { get; set; }

        [StringRange(AllowedValues = new string[] { "Pending", "Accepted", "Declined" }, ErrorMessage = "Choice unknown.")]
        public string Acceptance { get; set; } = "Pending";

        [DataType(DataType.Text)]
        public string Message { get; set; } = string.Empty;

        public long EventId { get; set; }

        public virtual Event? Event { get; set; }

        public string MemberId { get; set; } = string.Empty;

        public virtual ApplicationUser? Member { get; set; }
    }
}