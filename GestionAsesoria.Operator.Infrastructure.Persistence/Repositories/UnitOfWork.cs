using GestionAsesoria.Operator.Application.Interfaces.Repositories;
using GestionAsesoria.Operator.Application.Interfaces.Repositories.Projects;
using GestionAsesoria.Operator.Application.Interfaces.Services;
using GestionAsesoria.Operator.Application.Interfaces.Services.ExternalRequest;
using GestionAsesoria.Operator.Domain.Auditable;
using GestionAsesoria.Operator.Infrastructure.Persistence.Contexts;
using GestionAsesoria.Operator.Infrastructure.Persistence.Repositories;
using GestionAsesoria.Operator.Infrastructure.Persistence.Repositories.Projects;
using GestionAsesoria.Operator.Infrastructure.Persistence.Repository;
using LazyCache;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Infrastructure.Repositories
{
    public class UnitOfWork<TId> : IUnitOfWork<TId>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _dbContext;
        private bool disposed;
        private Hashtable _repositories;
        private readonly IAppCache _cache;

        public UnitOfWork(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IAppCache cache)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _currentUserService = currentUserService;
            _cache = cache;
        }

        public IGenericRepositoryAsync<TEntity, TId> Repository<TEntity>() where TEntity : AuditableEntity<TId>
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepositoryAsync<,>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TId)), _dbContext);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepositoryAsync<TEntity, TId>)_repositories[type]!;
        }

        public IEmailService EmailService => null!;
        public IMasterDataValueRepositoryAsync _masterDataValue => null!;
        public IActorRepositoryAsync _actor => null!;
        public IAdvisoringContractRepositoryAsync _AdvisoringContract => null!;
        public IAdvisoringRequestRepositoryAsync _advisoringRequestRepository => null!;
        public IProjectRepositoryAsync _project => null;



        public IMasterDataValueRepositoryAsync MasterDataValueRepository => _masterDataValue ?? new MasterDataValueRespositoryAsync(_dbContext);
        public IActorRepositoryAsync ActorRepository => _actor ?? new ActorRepositoryAsync(_dbContext);
        public IAdvisoringContractRepositoryAsync AdvisoringContractRepository => _AdvisoringContract ?? new AdvisoringContractsRepositoryAsync(_dbContext);
        public IAdvisoringRequestRepositoryAsync AdvisoringRequestRepository => _advisoringRequestRepository ?? new AdvisoringRequestRepositoryAsync(_dbContext);
        public IProjectRepositoryAsync ProjectRepository => _project ?? new ProjectRepositoryAsync(_dbContext);



        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys)
        {
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            foreach (var cacheKey in cacheKeys)
            {
                _cache.Remove(cacheKey);
            }
            return result;
        }

        public Task Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            return Task.CompletedTask;
        }

        public IDbTransaction BeginTransaction()
        {
            var transaction = _dbContext.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //dispose managed resources
                    _dbContext.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }

    }
}