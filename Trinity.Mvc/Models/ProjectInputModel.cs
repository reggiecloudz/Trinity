#nullable disable
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Infrastructure.Validation;

namespace Trinity.Mvc.Models
{
    public class ProjectInputModel
    {
        public string Name { get; set; }

        [Precision(8, 2)]
        [DisplayName("Fundraising Goal")]
        public decimal FinancialGoal { get; set; } = 0.0m;

        [Required]
        [NotMapped]
        [FileExtension]
        public IFormFile PhotoUpload { get; set; }

        public long CauseId { get; set; }

        public long CityId { get; set; }

        public SelectList Causes { get; set; }
        public SelectList States { get; set; }
    }
}