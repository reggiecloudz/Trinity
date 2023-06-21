using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class ConnectionRequest : Entity
    {
        [Key]
        public long Id { get; set; }

        public string RequesterId { get; set; } = string.Empty;

        [ForeignKey("RequesterId")]
        public virtual ApplicationUser? Requester { get; set; }

        public string ReceiverId { get; set; } = string.Empty;

        [ForeignKey("ReceiverId")]
        public virtual ApplicationUser? Receiver { get; set; }
    }
}