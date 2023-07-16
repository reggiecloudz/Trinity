using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Models
{
    public class SceneInteractionModel
    {
        public long SceneId { get; set; }
        public bool IsLiked { get; set; }
        public long Likes { get; set; }
    }
}