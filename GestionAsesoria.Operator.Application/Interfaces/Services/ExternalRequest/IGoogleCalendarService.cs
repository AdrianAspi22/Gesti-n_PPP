using GestionAsesoria.Operator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest
{
    public interface IGoogleCalendarService
    {
        Task<string> AuthorizeAsync(string userId);
        Task<GoogleCalendarEvent> CreateEventAsync(GoogleCalendarEvent calendarEvent, string userId);
        Task<List<GoogleCalendarEvent>> GetEventsAsync(DateTime startDate, DateTime endDate, string userId);
        Task<bool> DeleteEventAsync(string eventId, string userId);
        Task<GoogleCalendarEvent> UpdateEventAsync(GoogleCalendarEvent calendarEvent, string userId);
    }
}
