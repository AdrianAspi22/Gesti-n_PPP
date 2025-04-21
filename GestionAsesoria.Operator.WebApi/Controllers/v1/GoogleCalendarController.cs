using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.WebApi.Controllers.v1
{
    public class GoogleCalendarController : BaseApiController<GoogleCalendarController>
    {
        private readonly IGoogleCalendarService _googleCalendarService;

        public GoogleCalendarController(IGoogleCalendarService googleCalendarService)
        {
            _googleCalendarService = googleCalendarService;
        }

        [HttpGet("authorize")]
        public async Task<IActionResult> Authorize([FromQuery] string userId)
        {
            var token = await _googleCalendarService.AuthorizeAsync(userId);
            return Ok(token);
        }

        [Authorize(Policy = Permissions.GoogleCalendars.Create)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateEvent(
            [FromBody] GoogleCalendarEvent calendarEvent,
            [FromQuery] string userId)
        {
            var createdEvent = await _googleCalendarService.CreateEventAsync(calendarEvent, userId);
            return Ok(createdEvent);
        }

        [Authorize(Policy = Permissions.GoogleCalendars.View)]
        [HttpGet("events")]
        public async Task<IActionResult> GetEvents(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate,
            [FromQuery] string userId)
        {
            var events = await _googleCalendarService.GetEventsAsync(startDate, endDate, userId);
            return Ok(events);
        }

        [Authorize(Policy = Permissions.GoogleCalendars.Delete)]
        [HttpDelete("delete/{eventId}")]
        public async Task<IActionResult> DeleteEvent(
            string eventId,
            [FromQuery] string userId)
        {
            var result = await _googleCalendarService.DeleteEventAsync(eventId, userId);
            return Ok(result);
        }

        [Authorize(Policy = Permissions.GoogleCalendars.Edit)]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateEvent(
            [FromBody] GoogleCalendarEvent calendarEvent,
            [FromQuery] string userId)
        {
            var updatedEvent = await _googleCalendarService.UpdateEventAsync(calendarEvent, userId);
            return Ok(updatedEvent);
        }
    }
}
