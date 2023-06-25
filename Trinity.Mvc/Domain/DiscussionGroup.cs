using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Domain
{
    public class DiscussionGroup : Entity
    {
        public DiscussionGroup() {}
        
        public long Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Description { get; set; } = string.Empty;

        public string ModeratorId { get; set; } = string.Empty;
        public virtual ApplicationUser? Moderator { get; set; }

        public long CategoryId { get; set; }
        public virtual Category? Category { get; set; }

        public virtual ICollection<Topic> Topics { get; set; } = new List<Topic>();
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Subscription> Subscribers { get; set; } = new List<Subscription>();
    }
}