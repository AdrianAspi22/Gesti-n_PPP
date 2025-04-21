using GestionAsesoria.Operator.Domain.Auditable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GestionAsesoria.Operator.Application.Interfaces.Repositories
{
    public interface IGenericRepositoryAsync<T, in TId> where T : class, IEntity<TId>
    {
        Task<IEnumerable<T>> GetPagedReponseAsync(int pageNumber, int pageSize);
        //Task<IEnumerable<T>> GetPagedAdvancedReponseAsync(int pageNumber, int pageSize, string orderBy, string fields);
        IQueryable<T> GetAllQueryable();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetSelectAsync();
        Task<IEnumerable<T>> GetSelectByIdAsync(TId id);
        Task<T> GetByIdAsync(TId id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeletePermanentAsync(T entity);
        Task<bool> ChangeStateAsync(int id);
        Task ChangeStateRegisterAsync(T entity);
        Task<T> GetLastCodeRegisterAsync();
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> Entities { get; }
    }
}