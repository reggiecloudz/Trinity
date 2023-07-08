using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class City : Entity
    {
        public long Id { get; set; }

        public long StateId { get; set; }
        public virtual State? State { get; set; }

        public string Name { get; set; } = string.Empty;

        public string County { get; set; } = string.Empty;

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}