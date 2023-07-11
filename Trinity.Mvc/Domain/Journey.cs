using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Journey : Entity
    {
        public long Id { get; set; }

        public virtual ICollection<Scene> Scenes { get; set; } = new List<Scene>();
    }
}