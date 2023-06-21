using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Post : Entity
    {
        public Post() {}
        
        public long Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Body { get; set; } = string.Empty;

        public long TopicId { get; set; }
        public virtual Topic? Topic { get; set; }

        public long DiscussionGroupId { get; set; }
        public virtual DiscussionGroup? DiscussionGroup { get; set; }

        public string AuthorId { get; set; } = string.Empty;
        public virtual ApplicationUser? Author { get; set; }

        public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
    }
}