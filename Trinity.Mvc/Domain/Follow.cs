using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Follow : Entity
    {
        [Key]
        public long Id { get; set; }
        
        public string FollowerId { get; set; } = string.Empty;
        
        [ForeignKey("FollowerId")]
        public virtual ApplicationUser? Follower { get; set; }

        public string FollowingId { get; set; } = string.Empty;

        [ForeignKey("FollowingId")]
        public virtual ApplicationUser? Following { get; set; }
    }
}