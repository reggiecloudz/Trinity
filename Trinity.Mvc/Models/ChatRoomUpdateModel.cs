using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Models
{
    public class ChatRoomUpdateModel
    {
        public long ChatId { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<SelectableConnection> Connections { get; set; } = new List<SelectableConnection>();

        public string[] SelectedUsers { get; set; } = new string[]{};
    }
}