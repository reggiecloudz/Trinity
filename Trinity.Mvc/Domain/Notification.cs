using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Domain
{
    public class Notification : Entity
    {
        public long Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public virtual List<UserNotification> Users { get; set; } = new List<UserNotification>();
    }
}