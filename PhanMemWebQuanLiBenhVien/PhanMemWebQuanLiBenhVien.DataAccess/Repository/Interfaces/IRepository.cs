using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemWebQuanLiBenhVien.DataAccess.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //getall 
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
                              Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                              string? includeProperty = null,
                              int? take = null);
        //get
        T Get(Expression<Func<T, bool>>? filter = null, string? includeProperty = null, bool tracked = false);

        //add 
        void Add(T entity);

        //remove 
        void Remove(T entity);

        //remove range
        void RemoveRange(IEnumerable<T> entities);
    }
}
