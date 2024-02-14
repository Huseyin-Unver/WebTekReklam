using ApplicationCore_WebReklam.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_WebReklam.Services.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        //Read Operations
        Task<T> GetById(int id);
        Task<T> GetByDefault(Expression<Func<T, bool>> expression);
        Task<List<T>> GetByDefaults(Expression<Func<T, bool>> expression);
        Task<bool> Any(Expression<Func<T, bool>> expression);

        Task<List<TResult>> GetFilteredList<TResult>
            (
                Expression<Func<T, TResult>> select,
                Expression<Func<T, bool>> where = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null
            );
    }
}
