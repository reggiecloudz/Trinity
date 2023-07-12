using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Like : Entity
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public LikableType Type { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }
    }
}