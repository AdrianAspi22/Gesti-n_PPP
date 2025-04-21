namespace GestionAsesoria.Operator.Domain.Auditable
{
    public abstract class AuditableEntity<TId> : IAuditableEntity<TId>
    {
        public TId Id { get; set; }
        //public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
        //public string? CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public string? LastModifiedBy { get; set; }
        //public DateTime? LastModifiedOn { get; set; }
        //public string? DeletedBy { get; set; }
        //public DateTime? DeletedOn { get; set; }

    }
}
