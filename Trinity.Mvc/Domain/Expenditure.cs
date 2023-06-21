using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Expenditure : Entity
    {
        public Expenditure() {}
        
        public long Id { get; set; }

        public string Slug { get; set; } = string.Empty;

        public string Item { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Purpose { get; set; } = string.Empty;

        [Precision(11, 2)]
        public decimal Cost { get; set; } = 0.00m;

        public long ProjectId { get; set; }
        public Project? Project { get; set; }
    }
}