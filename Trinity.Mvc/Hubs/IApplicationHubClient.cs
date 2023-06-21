using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trinity.Mvc.Hubs
{
    public interface IApplicationHubClient
    {
        Task ReceiveMessage(string message);
    }
}