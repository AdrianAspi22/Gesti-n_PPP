using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa el calendario que se integrará de Google, el cual incluye detalles sobre tokens de acceso, tipo de eventos y URL de programación.")]
    public class Calendar : AuditableEntity<int>
    {
        [Comment("URI del usuario asociado al calendario.")]
        public string UserUri { get; set; }

        [Comment("Token de acceso para el calendario.")]
        public string AccessToken { get; set; }

        [Comment("Token de actualización para el calendario.")]
        public string RefreshToken { get; set; }

        [Comment("Fecha de expiración del token de acceso, puede ser nula si no se ha establecido.")]
        public DateTime? AccessTokenExpiration { get; set; }

        [Comment("Fecha de expiración del token de actualización, puede ser nula si no se ha establecido.")]
        public DateTime? RefreshTokenExpiration { get; set; }

        [Comment("Tipo de evento asociado al calendario, puede ser nulo.")]
        public string? EventType { get; set; }

        [Comment("Token de la página de eventos, puede ser nulo.")]
        public string? EventsPageToken { get; set; }

        [Comment("URL de programación del calendario, puede ser nula.")]
        public string? SchedulingUrl { get; set; }

        [Comment("Identificador del actor asociado al calendario.")]
        public int ActorId { get; set; }

        [Comment("Actor asociado al calendario.")]
        public virtual Actor Actor { get; set; }
    }
}
