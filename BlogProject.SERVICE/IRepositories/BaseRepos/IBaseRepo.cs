using BlogProject.CORE.CoreModels.BaseModels;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.SERVICE.IRepositories.BaseRepos
{
    public interface IBaseRepo<T> where T : IBaseEntity
    {
        Task<T> GetByIdAsync(string id);
        T GetById(string id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<TResult> GetFilteredModelAysnc<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null);

        Task<IEnumerable<TResult>> GetFilteredModelListAysnc<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null);

        Task<int> CountAsync();

        int Add(T entity);
        Task<int> AddAsync(T entity);
        int Update(T entity);
        Task<int> UpdateAsync(T entity);
        int Delete(T entity);
        Task<int> DeleteAsync(T entity);

    }
}
