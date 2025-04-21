using System;
using System.ComponentModel.DataAnnotations;

namespace GestionAsesoria.Operator.Domain.Entities
{
    public class GoogleCalendarEvent
    {
        public string Id { get; set; }
        [Required]
        public string Summary { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public string TimeZone { get; set; } = "America/Bogota";
    }
}
