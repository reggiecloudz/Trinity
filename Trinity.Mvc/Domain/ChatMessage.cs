using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class ChatMessage : Entity
    {
        public long Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }

        public long ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
