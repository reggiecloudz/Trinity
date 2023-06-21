using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Domain
{
    public class ProjectSupporter : Entity
    {
        [StringRange(AllowedValues = new string[] { "Manager", "Staff", "Supporter", "Donor" }, ErrorMessage = "Choice unknown.")]
        public string ProjectRole { get; set; } = "Supporter";

        public string Position { get; set; } = "Supporter";

        public long ProjectId { get; set; }
        public Project? Project { get; set; }

        public string SupporterId { get; set; } = string.Empty;
        public virtual ApplicationUser? Supporter { get; set; }
    }
}