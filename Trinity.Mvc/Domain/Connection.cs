using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Connection : Entity
    {
        [Key]
        public long Id { get; set; }
        
        public string ConnectorId { get; set; } = string.Empty;
        
        [ForeignKey("ConnectorId")]
        public virtual ApplicationUser? Connector { get; set; }

        public string ConnectId { get; set; } = string.Empty;

        [ForeignKey("ConnectId")]
        public virtual ApplicationUser? Connect { get; set; }
    }
}