using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Reply : Entity
    {
        public long Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public long PostId { get; set; }

        public virtual Post? Post { get; set; }

        public long? ParentId { get; set; }

        public virtual Reply? Parent { get; set; }

        public string AuthorId { get; set; } = string.Empty;
        public virtual ApplicationUser? Author { get; set; }

        public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();
        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}