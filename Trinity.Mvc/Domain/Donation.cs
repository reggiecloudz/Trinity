using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Donation : Entity
    {
        public long Id { get; set; }
        
        [Precision(11, 2)]
        public decimal Amount { get; set; }

        [DataType(DataType.Text)]
        public string Message { get; set; } = string.Empty;

        public string DonorId { get; set; } = string.Empty;
        public virtual ApplicationUser? Donor { get; set; }

        public long FundraiserId { get; set; }
        public virtual Fundraiser? Fundraiser { get; set; }
    }
}