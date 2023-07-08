using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Data.Repository
{
    public interface IChatRepository
    {
        ChatMessageViewModel GetMessageById(long id);
        Task<List<SelectableConnection>> GetSelectableUsers(Chat chat, string currentUserId);
        Task RemoveUnchecked(string[] userIds, Chat chat);
        ChatRole GetChatUserRole(Chat chat, string userId);
    }
}