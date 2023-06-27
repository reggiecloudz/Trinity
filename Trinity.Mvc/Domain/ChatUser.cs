using System;

namespace Trinity.Mvc.Domain
{
    public class ChatUser
    {
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser? User { get; set; }

        public long ChatId { get; set; }
        public virtual Chat? Chat { get; set; }
        
        public ChatRole Role { get; set; }
    }
}
