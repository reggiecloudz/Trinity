using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class DonationInputModel
    {
        public DonationInputModel() {}

        [Precision(8, 2)]
        public decimal Amount { get; set; }

        [DataType(DataType.Text)]
        public string Message { get; set; } = string.Empty;

        public long FundraiserId { get; set; }

        public long ProjectId { get; set; }
    }
}