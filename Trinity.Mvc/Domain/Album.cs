using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Album : Entity
    {
        public long Id { get; set; }
        public virtual ICollection<MediaFile> Files { get; set; } = new List<MediaFile>();
        public long ProjectId { get; set; }
        public virtual Project? Project { get; set; }
    }
}