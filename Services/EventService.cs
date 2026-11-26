using EventRegistrationAPI_SaadAllahwala.Data;
using EventRegistrationAPI_SaadAllahwala.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationAPI_SaadAllahwala.Services


{
    public class EventService : IEventService
    {

        private readonly AppDbContext _context;

        public EventService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EventDto>> GetAllEventsAsync()
        {
            return await _context.Events
                .OrderBy(e => e.EventDate)
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Location = e.Location,
                    EventDate = e.EventDate,
                    Capacity = e.Capacity,
                    RegisteredCount = e.Registrations.Count(),
                    SpotsLeft = e.Capacity - e.Registrations.Count()
                })
                .ToListAsync();
        }

        public async Task<EventDto?> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Where(e => e.EventId == id)
                .Select(e => new EventDto
                {
                    EventId = e.EventId,
                    Title = e.Title,
                    Location = e.Location,
                    EventDate = e.EventDate,
                    Capacity = e.Capacity,
                    RegisteredCount = e.Registrations.Count(),
                    SpotsLeft = e.Capacity - e.Registrations.Count()
                })
                .FirstOrDefaultAsync();
        }
    }
}
