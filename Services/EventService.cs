using EventRegistrationAPI_SaadAllahwala.Data;
using EventRegistrationAPI_SaadAllahwala.DTOs;
using EventRegistrationAPI_SaadAllahwala.Models;
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

        public async Task<EventDto> CreateEventAsync(CreateEventDto dto)
        {
            var campusEvent = new Event
            {
                Title = dto.Title,
                Location = dto.Location,
                EventDate = dto.EventDate,
                Capacity = dto.Capacity
            };

            _context.Events.Add(campusEvent);
            await _context.SaveChangesAsync();

            return new EventDto
            {
                EventId = campusEvent.EventId,
                Title = campusEvent.Title,
                Location = campusEvent.Location,
                EventDate = campusEvent.EventDate,
                Capacity = campusEvent.Capacity,
                RegisteredCount = 0,
                SpotsLeft = campusEvent.Capacity
            };
        }
    }
}
