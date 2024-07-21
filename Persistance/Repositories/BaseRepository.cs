using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly FinancialAppDBContext _context;
        private readonly DbSet<TEntity> _set;

        protected BaseRepository(FinancialAppDBContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Set => _set;

        public async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> expression,
         Expression<Func<TEntity, object>>[] includeExpressions = null,
       CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _set.Where(expression);
         
            if (includeExpressions != null)
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }     
            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) =>
            _set.Where(expression).ToListAsync(cancellationToken);
        public async Task<List<TEntity>> GetAllPaginatedAsync(
         Expression<Func<TEntity, bool>> expression,
         int pageNumber,
         int pageSize,
         Expression<Func<TEntity, object>>[] includeExpressions = null,
         CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<TEntity> query = _set.Where(expression);
                if (includeExpressions != null)
                {
                    foreach (var includeExpression in includeExpressions)
                    {
                        query = query.Include(includeExpression);
                    }
                }
                int totalCount = await query.CountAsync(cancellationToken);

                int pageCount = totalCount > 0 ? (int)Math.Ceiling((double)totalCount / pageSize) : 0;

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }
                else if (pageNumber > pageCount && pageCount > 0)
                {
                    pageNumber = pageCount;
                }

                int skipAmount = (pageNumber - 1) * pageSize;

                List<TEntity> pagedItems = await query.Skip(skipAmount).Take(pageSize).ToListAsync(cancellationToken);


                return pagedItems;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error fetching paginated items.", ex);
            }
        }


        public Task<TEntity> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
            => _set.AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);
  
        public ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _set.AddAsync(entity, cancellationToken);
            return ValueTask.CompletedTask;
        }  
        public ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _set.Update(entity);
            return ValueTask.CompletedTask;
        }
  
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Remove(TEntity entity)
        {
            _set.Remove(entity);
        }

        public void RemoveRange(List<TEntity> entities)
        {
            _set.RemoveRange(entities);
        }
    }
}
