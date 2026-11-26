using EventRegistrationAPI_SaadAllahwala.DTOs;

namespace EventRegistrationAPI_SaadAllahwala.Services
{
    // Defines the available registration operations.
    public interface IRegistrationService
    {
        Task<List<RegistrationDto>?> GetRegistrationsAsync(int eventId);

        Task<RegistrationDto> CreateRegistrationAsync(int eventId,
            CreateRegistrationDto dto);

        Task<bool> DeleteRegistrationAsync(int id);
    }
}