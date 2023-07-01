using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Data.Repository
{
    public interface IEventRepository
    {
        List<EventViewModel> GetUserEvents(string userId);
        string EventsJsonSerializer(List<EventViewModel> events);
    }
}