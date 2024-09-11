using BlogProject.CORE.CoreModels.BaseModels;
using BlogProject.REPO.Contexts;
using BlogProject.REPO.Utilities.Logging;
using BlogProject.SERVICE.IRepositories.BaseRepos;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Repositories.BaseRepos
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public BaseRepo(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }

        public int Add(T entity)
        {
            _table.Add(entity);
            return _context.SaveChanges();

        }

        public async Task<int> AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
        {
            return await _table.AnyAsync(filter);
        }

        public async Task<int> CountAsync()
        {
            if (_table == null)
            {
                return 0;
            }

            return await _table.CountAsync();
        }

        public int Delete(T entity)
        {
            _table.Remove(entity);
            return _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _table.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter is null)
            {
                return await _table.ToListAsync();
            }
            else
            {
                return await _table.Where(filter).ToListAsync();
            }
        }

        public T GetById(string id)
        {
            return _table.Find(id);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<TResult> GetFilteredModelAysnc<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null)
        {
            IQueryable<T> query = _table;

            if (join != null)
                query = join(query);

            if (where != null)
                query = query.Where(where);

            if (orderBy != null)
            {
                return await orderBy(query).Select(select).FirstOrDefaultAsync();
            }
            else
                return await query.Select(select).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TResult>> GetFilteredModelListAysnc<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> join = null)
        {
            IQueryable<T> query = _table;

            if (join != null)
                query = join(query);

            if (where != null)
                query = query.Where(where);

            if (orderBy != null)
            {
                return await orderBy(query).Select(select).ToListAsync();
            }
            else
                return await query.Select(select).ToListAsync();
        }

        public int Update(T entity)
        {
            _table.Update(entity);
            return _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _table.Update(entity);
            return await _context.SaveChangesAsync();
        }
    }
}
