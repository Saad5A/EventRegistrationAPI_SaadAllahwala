using EventRegistrationAPI_SaadAllahwala.Models;
using Microsoft.EntityFrameworkCore;
namespace EventRegistrationAPI_SaadAllahwala.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<EventRegistrationAPI_SaadAllahwala.Models.Event> Events { get; set; } = null!;
        public DbSet<EventRegistrationAPI_SaadAllahwala.Models.Registration> Registrations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed campus events.
            modelBuilder.Entity<Event>().HasData(
                new Event
                {
                    EventId = 1,
                    Title = "Career Fair",
                    Location = "HMC Campus",
                    EventDate = new DateTime(2027, 3, 15),
                    Capacity = 50
                },
                new Event
                {
                    EventId = 2,
                    Title = "Programming Workshop",
                    Location = "Trafalgar Campus",
                    EventDate = new DateTime(2027, 4, 10),
                    Capacity = 20
                },
                new Event
                {
                    EventId = 3,
                    Title = "Networking Session",
                    Location = "Davis Campus",
                    EventDate = new DateTime(2027, 3, 20),
                    Capacity = 2
                },
                new Event
                {
                    EventId = 4,
                    Title = "Student Orientation",
                    Location = "HMC Campus",
                    EventDate = new DateTime(2026, 9, 5),
                    Capacity = 100
                }
            );

            // Fill event 3 to its maximum capacity.
            modelBuilder.Entity<Registration>().HasData(
                new Registration
                {
                    RegistrationId = 1,
                    EventId = 3,
                    StudentName = "John Smith",
                    Email = "john@example.com",
                    RegisteredOn = new DateTime(2026, 11, 20)
                },
                new Registration
                {
                    RegistrationId = 2,
                    EventId = 3,
                    StudentName = "Sarah Wilson",
                    Email = "sarah@example.com",
                    RegisteredOn = new DateTime(2026, 11, 21)
                }
            );
        }
    }
}
