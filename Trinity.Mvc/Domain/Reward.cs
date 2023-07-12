using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Reward : Entity
    {
        public long Id { get; set; }
        
        public string Item { get; set; } = string.Empty;
        
        public int Quantity { get; set; } = 1;
        
        [Precision(8, 2)]
        public decimal AmountNeeded { get; set; }

        public long FundraiserId { get; set; }
        public virtual Fundraiser? Fundraiser { get; set; }

        public virtual ICollection<UserReward> Recipients { get; set; } = new List<UserReward>();
    }
}