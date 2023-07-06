using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Data.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
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
    }
}