using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SUP_BAL.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllQueryableInclude(Expression<Func<T, bool>> predicate, string path);
        IQueryable<T> GetTwoPathQueryableInclude(Expression<Func<T, bool>> predicate, string path, string path1);
        IQueryable<T> GetThreePathQueryableInclude(Expression<Func<T, bool>> predicate, string path, string path1, string path2);
        IQueryable<T> GetForthPathQueryableInclude(Expression<Func<T, bool>> predicate, string path, string path1, string path2, string path3);
        T GetById(object id);
        T GetByIdQueryable(Expression<Func<T, bool>> predicate);
        T GetByIdQueryableInclude(Expression<Func<T, bool>> predicate, string path);
        T Insert(T obj);
        
        void InsertRange(List<T> obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> DeleteAll(Expression<Func<T, bool>> predicate);
        void Dispose(bool disposing);
    }
}
