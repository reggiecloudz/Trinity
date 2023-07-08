using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class State : Entity
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;

        public virtual ICollection<City> Cities { get; set; } = new List<City>();
    }
}