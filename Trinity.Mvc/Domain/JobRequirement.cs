using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class JobRequirement : Entity
    {
        public long Id { get; set; }
        
        public string Text { get; set; } = string.Empty;

        public long PositionId { get; set; }
        public virtual Position? Position { get; set; }

    }
}