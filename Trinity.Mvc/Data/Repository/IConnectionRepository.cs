using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Data.Repository
{
    public interface IConnectionRepository
    {
        Task SendRequest(string RequesterId, string ReceiverId);
    }
}