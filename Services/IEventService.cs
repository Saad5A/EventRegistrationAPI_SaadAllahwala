using System.Threading.Tasks;
using EventRegistrationAPI_SaadAllahwala.DTOs;

namespace EventRegistrationAPI_SaadAllahwala.Services
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllEventsAsync();

        Task<EventDto?> GetEventByIdAsync(int id);
    }
}
