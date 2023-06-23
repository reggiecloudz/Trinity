using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Category : Entity
    {
        public long Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<DiscussionGroup> DiscussionGroups { get; set; } = new List<DiscussionGroup>();
    }
}