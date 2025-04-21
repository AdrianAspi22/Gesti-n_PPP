using System.ComponentModel;

namespace GestionAsesoria.Operator.Domain.Enums
{
    public enum AdvisoringRequestStatus
    {
        [Description("SOLICITUD PENDIENTE")]
        PendingRequest,

        [Description("ACEPTADO")]
        Accepted,

        [Description("RECHAZADO")]
        Refused,
    }
}
