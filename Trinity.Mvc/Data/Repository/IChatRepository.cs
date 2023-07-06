using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Data.Repository
{
    public interface IChatRepository
    {
        ChatMessageViewModel GetMessageById(long id);
    }
}