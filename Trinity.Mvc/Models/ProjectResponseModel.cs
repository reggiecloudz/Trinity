using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class ProjectResponseModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cause { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}