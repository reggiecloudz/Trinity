using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Fundraiser : Entity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public string Purpose { get; set; } = string.Empty;

        [Precision(8, 2)]
        public decimal Goal { get; set; } = 0.00m;

        [Precision(8, 2)]
        public decimal AmountRaised { get; set; } = 0.00m;

        public long ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public virtual ICollection<Reward> Rewards { get; set; } = new List<Reward>();
    }
}