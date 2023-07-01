using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Trinity.Mvc.Models;

namespace Trinity.Mvc.Data.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<EventViewModel> GetUserEvents(string userId)
        {
            var eventsAttending = _context.EventAttendees
                .Where(e => e.AttendeeId == userId)
                .Include(e => e.Event)
                    .ThenInclude(e => e!.Project)
                .ToList();

            List<EventViewModel> events = new List<EventViewModel>();

            foreach (var item in eventsAttending)
            {
                events.Add(new EventViewModel
                {
                    Id = item.EventId,
                    Name = item.Event!.Name,
                    Location = item.Event!.Location,
                    Details = item.Event.Details,
                    ProjectName = item.Event.Project!.Name,
                    Start = item.Event.Start,
                    End = item.Event.End
                });
            }
            return events;
        }

        public string EventsJsonSerializer(List<EventViewModel> events)
        {
            var options = new JsonSerializerOptions 
            {
                // ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(events, options);
        }
    }
}