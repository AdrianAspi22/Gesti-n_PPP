using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Repositories
{
    public interface IUnitOfWorkTransaction : IDbTransaction
    {
        // Additional methods if needed for your tests
        new void Commit();
        Task CommitAsync(CancellationToken cancellationToken = default);
        new void Rollback();
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}