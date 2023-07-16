using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class LikeSceneResponseModel
    {
        public bool Liked { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}