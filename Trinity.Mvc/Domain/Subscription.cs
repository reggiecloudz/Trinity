using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Subscription : Entity
    {
        public long DiscussionGroupId { get; set; }
        public virtual DiscussionGroup? DiscussionGroup { get; set; }

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
}