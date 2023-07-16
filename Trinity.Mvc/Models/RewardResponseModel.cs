using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class RewardResponseModel
    {
        public long Id { get; set; }

        public string Item { get; set; } = string.Empty;

        public int Quantity { get; set; }

        [Precision(8, 2)]
        public decimal AmountNeeded { get; set; }

        public string Message { get; set; } = string.Empty;
    }
}