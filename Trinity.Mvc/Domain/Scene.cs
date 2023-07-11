using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Scene : Entity
    {
        public long Id { get; set; }

        public long JourneyId { get; set; }
        public virtual Journey? Journey { get; set; }
    }
}