#nullable disable
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Models
{
    public class ProjectInputModel
    {
        public Project Project { get; set; }

        public SelectList Causes { get; set; }
        public SelectList States { get; set; }
    }
}