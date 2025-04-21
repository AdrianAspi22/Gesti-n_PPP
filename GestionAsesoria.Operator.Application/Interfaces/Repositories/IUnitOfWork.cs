using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Domain.Auditable;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Repositories
{
    public interface IUnitOfWork<TId> : IDisposable
    {
        IGenericRepositoryAsync<T, TId> Repository<T>() where T : AuditableEntity<TId>;
        IEmailService EmailService { get; }
        IMasterDataValueRepositoryAsync MasterDataValueRepository { get; }
        IActorRepositoryAsync ActorRepository { get; }
        IAdvisoringContractRepositoryAsync AdvisoringContractRepository { get; }
        IAdvisoringRequestRepositoryAsync AdvisoringRequestRepository { get; }


        Task<int> Commit(CancellationToken cancellationToken);
        Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
        IDbTransaction BeginTransaction();
        Task Rollback();
    }
}