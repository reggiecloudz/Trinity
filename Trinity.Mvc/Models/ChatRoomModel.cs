using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Models
{
    public class ChatRoomModel
    {
        public ChatRoomModel() {}
        
        public string Name { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        //"assets/images/groupchat.png";
        public Chat? Chat { get; set; }
        public ChatRole ChatRole { get; set; }
    }
}