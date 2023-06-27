using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Chat : Entity
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

        public virtual ICollection<ChatUser> Users { get; set; } = new List<ChatUser>();

        public ChatType Type { get; set; }
    }

    public enum ChatType
    {
        Room,
        Private
    }

    
}
