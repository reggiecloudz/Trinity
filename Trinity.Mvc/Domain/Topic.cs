using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Topic : Entity
    {
        public Topic() {}
        
        public long Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public long DiscussionGroupId { get; set; }
        public virtual DiscussionGroup? DiscussionGroup { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}