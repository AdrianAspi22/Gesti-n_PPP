namespace GestionAsesoria.Operator.Domain.Auditable
{
    public interface IEntity<TId> : IEntity
    {
        public TId Id { get; set; }
    }

    public interface IEntity
    {
    }
}
