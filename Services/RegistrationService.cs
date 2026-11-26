using Microsoft.EntityFrameworkCore;
using EventRegistrationAPI_SaadAllahwala.Data;
using EventRegistrationAPI_SaadAllahwala.DTOs;
using EventRegistrationAPI_SaadAllahwala.Models;

namespace EventRegistrationAPI_SaadAllahwala.Services
{
    // Handles registration operations and business rules.
    public class RegistrationService : IRegistrationService
    {
        private readonly AppDbContext _context;

        public RegistrationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegistrationDto>?> GetRegistrationsAsync(int eventId)
        {
            var campusEvent = await _context.Events.FindAsync(eventId);

            if (campusEvent == null)
            {
                return null;
            }

            return await _context.Registrations
                .Where(r => r.EventId == eventId)
                .Select(r => new RegistrationDto
                {
                    RegistrationId = r.RegistrationId,
                    EventTitle = r.Event.Title,
                    StudentName = r.StudentName,
                    Email = r.Email,
                    RegisteredOn = r.RegisteredOn
                })
                .ToListAsync();
        }

        public async Task<RegistrationDto> CreateRegistrationAsync(
            int eventId, CreateRegistrationDto dto)
        {
            var campusEvent = await _context.Events
                .Include(e => e.Registrations)
                .FirstOrDefaultAsync(e => e.EventId == eventId);

            // Rule 1: The event must exist.
            if (campusEvent == null)
            {
                throw new KeyNotFoundException("Event not found.");
            }

            // Rule 2: Registration is not allowed for past events.
            if (campusEvent.EventDate <= DateTime.Now)
            {
                throw new ArgumentException(
                    "Registration is closed for past events.");
            }

            // Rule 3: Prevent duplicate emails regardless of case.
            if (campusEvent.Registrations.Any(r =>
                string.Equals(r.Email, dto.Email,
                    StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(
                    "This email is already registered for this event.");
            }

            // Rule 4: Do not exceed the event capacity.
            if (campusEvent.Registrations.Count >= campusEvent.Capacity)
            {
                throw new InvalidOperationException(
                    "This event is full.");
            }

            var registration = new Registration
            {
                EventId = eventId,
                StudentName = dto.StudentName,
                Email = dto.Email,
                RegisteredOn = DateTime.Now
            };

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return new RegistrationDto
            {
                RegistrationId = registration.RegistrationId,
                EventTitle = campusEvent.Title,
                StudentName = registration.StudentName,
                Email = registration.Email,
                RegisteredOn = registration.RegisteredOn
            };
        }

        public async Task<bool> DeleteRegistrationAsync(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);

            if (registration == null)
            {
                return false;
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}