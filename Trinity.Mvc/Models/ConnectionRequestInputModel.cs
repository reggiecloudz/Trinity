using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class ConnectionRequestInputModel
    {
        [Required]
        public string ReceiverId { get; set; } = string.Empty;
    }
}