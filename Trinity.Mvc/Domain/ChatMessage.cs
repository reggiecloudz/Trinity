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

        public string UserName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public long ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
    }
}
