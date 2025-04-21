using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Domain.Entities;
using GestionAsesoria.Operator.Infrastructure.Persistence.Configurations;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Persistence.Services.ExternalRequest
{
    public class GoogleCalendarService : IGoogleCalendarService
    {
        private readonly GoogleCalendarOptions _options;
        private readonly ILogger<GoogleCalendarService> _logger;

        public GoogleCalendarService(
            IOptions<GoogleCalendarOptions> options,
            ILogger<GoogleCalendarService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        private async Task<UserCredential> GetCredentialAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId), "El userId no puede ser nulo o vacío.");
            }

            if (string.IsNullOrEmpty(_options.ClientId) || string.IsNullOrEmpty(_options.ClientSecret))
            {
                throw new InvalidOperationException("ClientId o ClientSecret no están configurados.");
            }

            if (_options.Scopes == null || !_options.Scopes.Any())
            {
                throw new InvalidOperationException("Scopes no están configurados.");
            }

            try
            {
                var credPath = Path.Combine("GoogleCalendarTokens", userId);
                return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = _options.ClientId,
                        ClientSecret = _options.ClientSecret
                    },
                    _options.Scopes,
                    userId,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo credenciales");
                throw;
            }
        }


        private CalendarService CreateCalendarService(UserCredential credential)
        {
            return new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "AcademicAdvising"
            });
        }

        public async Task<string> AuthorizeAsync(string userId)
        {
            var credential = await GetCredentialAsync(userId);
            return credential.Token.AccessToken;
        }

        public async Task<GoogleCalendarEvent> CreateEventAsync(GoogleCalendarEvent calendarEvent, string userId)
        {
            var credential = await GetCredentialAsync(userId);
            var service = CreateCalendarService(credential);

            var googleEvent = new Event
            {
                Summary = calendarEvent.Summary,
                Description = calendarEvent.Description,
                Start = new EventDateTime
                {
                    DateTime = calendarEvent.StartTime,
                    TimeZone = calendarEvent.TimeZone
                },
                End = new EventDateTime
                {
                    DateTime = calendarEvent.EndTime,
                    TimeZone = calendarEvent.TimeZone
                },
                Location = calendarEvent.Location
            };

            var insertRequest = service.Events.Insert(googleEvent, "primary");
            var insertedEvent = await insertRequest.ExecuteAsync();

            return new GoogleCalendarEvent
            {
                Id = insertedEvent.Id,
                Summary = insertedEvent.Summary,
                Description = insertedEvent.Description,
                StartTime = insertedEvent.Start.DateTime ?? DateTime.MinValue,
                EndTime = insertedEvent.End.DateTime ?? DateTime.MinValue,
                Location = insertedEvent.Location
            };
        }

        public async Task<List<GoogleCalendarEvent>> GetEventsAsync(DateTime startDate, DateTime endDate, string userId)
        {
            var credential = await GetCredentialAsync(userId);
            var service = CreateCalendarService(credential);

            var request = service.Events.List("primary");
            request.TimeMin = startDate;
            request.TimeMax = endDate;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var events = await request.ExecuteAsync();

            return events.Items.Select(e => new GoogleCalendarEvent
            {
                Id = e.Id,
                Summary = e.Summary,
                Description = e.Description,
                StartTime = e.Start.DateTime ?? DateTime.MinValue,
                EndTime = e.End.DateTime ?? DateTime.MinValue,
                Location = e.Location
            }).ToList();
        }

        public async Task<bool> DeleteEventAsync(string eventId, string userId)
        {
            var credential = await GetCredentialAsync(userId);
            var service = CreateCalendarService(credential);

            await service.Events.Delete("primary", eventId).ExecuteAsync();
            return true;
        }

        public async Task<GoogleCalendarEvent> UpdateEventAsync(GoogleCalendarEvent calendarEvent, string userId)
        {
            var credential = await GetCredentialAsync(userId);
            var service = CreateCalendarService(credential);

            var googleEvent = new Event
            {
                Id = calendarEvent.Id,
                Summary = calendarEvent.Summary,
                Description = calendarEvent.Description,
                Start = new EventDateTime
                {
                    DateTime = calendarEvent.StartTime,
                    TimeZone = calendarEvent.TimeZone
                },
                End = new EventDateTime
                {
                    DateTime = calendarEvent.EndTime,
                    TimeZone = calendarEvent.TimeZone
                },
                Location = calendarEvent.Location
            };

            var updateRequest = service.Events.Update(googleEvent, "primary", calendarEvent.Id);
            var updatedEvent = await updateRequest.ExecuteAsync();

            return new GoogleCalendarEvent
            {
                Id = updatedEvent.Id,
                Summary = updatedEvent.Summary,
                Description = updatedEvent.Description,
                StartTime = updatedEvent.Start.DateTime ?? DateTime.MinValue,
                EndTime = updatedEvent.End.DateTime ?? DateTime.MinValue,
                Location = updatedEvent.Location
            };
        }
    }
}
