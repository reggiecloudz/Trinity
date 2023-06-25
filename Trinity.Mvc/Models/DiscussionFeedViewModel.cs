using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Models
{
    public class DiscussionFeedViewModel
    {
        public DiscussionGroup? DiscussionGroup { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}