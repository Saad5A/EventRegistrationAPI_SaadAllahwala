using Microsoft.AspNetCore.Mvc;
using EventRegistrationAPI_SaadAllahwala.Services;
using EventRegistrationAPI_SaadAllahwala.DTOs;

namespace EventRegistrationAPI_SaadAllahwala.Controllers
{
    // Handles HTTP requests related to campus events.
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EventDto>>> GetEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetEvent(int id)
        {
            var campusEvent = await _eventService.GetEventByIdAsync(id);

            if (campusEvent == null)
            {
                return NotFound();
            }

            return Ok(campusEvent);
        }
    }
}