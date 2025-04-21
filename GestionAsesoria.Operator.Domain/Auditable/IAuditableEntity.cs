namespace GestionAsesoria.Operator.Domain.Auditable
{
    public interface IAuditableEntity<TId> : IAuditableEntity, IEntity<TId>
    {
    }

    public interface IAuditableEntity : IEntity
    {
        //bool IsActive { get; set; }
        //bool IsDeleted { get; set; }
        //string? CreatedBy { get; set; }
        //DateTime CreatedOn { get; set; }
        //string? LastModifiedBy { get; set; }
        //DateTime? LastModifiedOn { get; set; }
        //string? DeletedBy { get; set; }
        //DateTime? DeletedOn { get; set; }

    }
}
