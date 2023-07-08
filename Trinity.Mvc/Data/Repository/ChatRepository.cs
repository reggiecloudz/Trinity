using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Data.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConnectionRepository _connectionRepository;

        public ChatRepository(ApplicationDbContext context, IConnectionRepository connectionRepository)
        {
            _context = context;
            _connectionRepository = connectionRepository;
        }

        public ChatRole GetChatUserRole(Chat chat, string userId)
        {
            var chatUser = chat.Users.Where(c =>c.UserId == userId).FirstOrDefault();

            return chatUser!.Role;
        }

        public ChatMessageViewModel GetMessageById(long id)
        {
            var message = _context.ChatMessages.Include(m => m.User).FirstOrDefault(m => m.Id == id);
            var model = new ChatMessageViewModel 
            { 
                FullName = message!.User!.FullName,
                UserName = message.User.UserName,
                ProfileImage = message.User.ProfileImage,
                Content = message.Content,
                Created = message.Created.ToString("HH:mm tt")
            };
            return model;
        }

        public async Task<List<SelectableConnection>> GetSelectableUsers(Chat chat, string currentUserId)
        {
            var connections = await _connectionRepository.GetUserConnections(currentUserId);
            var roomMembers = new HashSet<string>(chat.Users.Select(u => u.UserId));
            var connectionList = new List<SelectableConnection>();

            foreach (var item in connections)
            {
                connectionList.Add(new SelectableConnection
                {
                    Value = item.Id,
                    Text = item.FullName,
                    UserPhoto = $"media/members/{item.ProfileImage}",
                    IsChecked = roomMembers.Contains(item.Id)
                });
            }

            return connectionList;
        }

        public async Task RemoveUnchecked(string[] userIds, Chat chat)
        {
            var roomMembers = new HashSet<string>(chat.Users.Where(u => u.Role != ChatRole.Admin).Select(u => u.UserId));
            var userIdsList = userIds.ToList();

            if (userIdsList != null)
            {
                foreach (var item in roomMembers)
                {
                    if (!userIdsList.Contains(item))
                    {
                        var chatUser = await _context.ChatUsers
                            .Where(c => c.ChatId == chat.Id && c.UserId == item)
                            .FirstOrDefaultAsync();
                        
                        if (chatUser != null)
                        {
                            _context.ChatUsers.Remove(chatUser);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}