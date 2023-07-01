using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class EventViewModel
    {
        public long Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ProjectName { get; set; } = string.Empty;

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}