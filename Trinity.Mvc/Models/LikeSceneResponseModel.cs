using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class LikeSceneResponseModel
    {
        public long SceneId { get; set; }
        public bool IsLiked { get; set; }
        public long Likes { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}