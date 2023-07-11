using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Comment : Entity
    {
        public long Id { get; set; }
        
        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public long SceneId { get; set; }

        public virtual Scene? Scene { get; set; }

        public long? ParentId { get; set; }

        public virtual Reply? Parent { get; set; }

        public string AuthorId { get; set; } = string.Empty;
        public virtual ApplicationUser? Author { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}