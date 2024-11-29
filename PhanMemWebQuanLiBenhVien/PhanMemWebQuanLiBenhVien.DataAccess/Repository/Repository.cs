using PhanMemWebQuanLiBenhVien.DataAccess;
using PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext _db;
        DbSet<T> _dbSet; 

        public Repository(ApplicationDbContext db)
        {
            this._db = db;
            this._dbSet = db.Set<T>(); 
        }
        public void Add(T entity)
        {
            _db.Add(entity);
        }

        public T Get(Expression<Func<T, bool>>? filter = null, 
                     string? includeProperty = null, 
                     bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked == false)
            {
                query = _dbSet.AsNoTracking();
            }
            else
            {
                query = _dbSet; 
            }

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperty))
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                     string? includeProperty = null,
                                     int? take = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (!string.IsNullOrEmpty(includeProperty))
            {
                query = query.Include(includeProperty);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
	}
}
