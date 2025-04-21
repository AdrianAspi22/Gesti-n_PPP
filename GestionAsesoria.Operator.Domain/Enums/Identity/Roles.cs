using System.ComponentModel;

namespace GestionAsesoria.Operator.Domain.Enums.Identity
{
    public enum Roles
    {
        [Description("SUPERADMIN")]
        SuperAdmin,

        [Description("ADMIN")]
        Admin,
    }
}