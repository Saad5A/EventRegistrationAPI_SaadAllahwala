using Microsoft.AspNetCore.Mvc;
using EventRegistrationAPI_SaadAllahwala.DTOs;
using EventRegistrationAPI_SaadAllahwala.Services;

namespace EventRegistrationAPI_SaadAllahwala.Controllers
{
    // Handles HTTP requests related to registrations.
    [ApiController]
    [Route("api")]
    public class RegistrationsController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationsController(
            IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet("events/{id}/registrations")]
        public async Task<ActionResult<List<RegistrationDto>>>
            GetRegistrations(int id)
        {
            var registrations =
                await _registrationService.GetRegistrationsAsync(id);

            if (registrations == null)
            {
                return NotFound();
            }

            return Ok(registrations);
        }

        [HttpPost("events/{id}/registrations")]
        public async Task<ActionResult<RegistrationDto>>
            CreateRegistration(int id, CreateRegistrationDto dto)
        {
            try
            {
                var registration = await _registrationService
                    .CreateRegistrationAsync(id, dto);

                return StatusCode(201, registration);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("registrations/{id}")]
        public async Task<IActionResult> DeleteRegistration(int id)
        {
            var deleted =
                await _registrationService.DeleteRegistrationAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}