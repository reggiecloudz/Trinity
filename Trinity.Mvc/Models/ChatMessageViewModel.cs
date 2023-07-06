using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class ChatMessageViewModel
    {
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string ProfileImage { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Created { get; set; } = string.Empty;
    }
}