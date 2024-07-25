using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IBaseRepository<TEntity>
    {
        IQueryable<TEntity> Set { get; }
               
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>>[] includeExpressions = null, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<(List<TEntity> PagedItems, int PageCount)> GetAllPaginatedAsync(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize, Expression<Func<TEntity, object>>[] includeExpressions = null, CancellationToken cancellationToken = default);
        Task<TEntity> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);     
        ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default);  
        ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default); 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);

    }
}
