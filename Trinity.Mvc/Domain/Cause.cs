using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Cause : Entity
    {
        public long Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public long? ParentId { get; set; }
        public virtual Cause? Parent { get; set; }

        public virtual ICollection<Cause> Children { get; set; } = new List<Cause>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}