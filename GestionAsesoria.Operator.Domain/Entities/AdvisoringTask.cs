using GestionAsesoria.Operator.Domain.Auditable;
using Microsoft.EntityFrameworkCore;

namespace GestionAsesoria.Operator.Domain.Entities
{
    [Comment("Representa una tarea de asesoría, que incluye detalles sobre la tarea asignada, explicaciones del Asesor y respuestas del estudiante.")]
    public class AdvisoringTask : AuditableEntity<int>
    {
        [Comment("Nombre de la tarea de asesoría.")]
        public string Name { get; set; }

        [Comment("Archivos adjuntos en formato JSON.")]
        public string AttachmentsJson { get; set; }

        [Comment("Explicación del asesor sobre la tarea.")]
        public string AdvisorExplanation { get; set; }

        [Comment("Respuesta del estudiante a la tarea.")]
        public string StudentResponse { get; set; }

        [Comment("Revisión de la tarea, puede ser nula si aún no se ha revisado, eso es lo que indica el símbolo \"?\" ")]
        public string? Review { get; set; }

        //Mapeo para llave Foranea
        public int AdvisingSessionId { get; set; }

        [Comment("Sesión de asesoría asociada a la tarea.")]
        public virtual AdvisingSession AdvisingSession { get; set; }
    }
}
