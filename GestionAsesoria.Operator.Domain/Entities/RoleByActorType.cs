using GestionAsesoria.Operator.Domain.Auditable;

namespace GestionAsesoria.Operator.Domain.Entities
{
    public class RoleByActorType : AuditableEntity<int>
    {
        public int RoleId { get; set; }
        public int ActorTypeId { get; set; }
        public bool IsActived { get; set; }
        public virtual Role Role { get; set; }
        public virtual ActorType ActorType { get; set; }
    }
}
