using SUP_BAL.IRepository;
using SUP_DAL.EFContextProvider;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SUP_BAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private SupplierDbContext _context = null;
        private DbSet<T> table = null;

        public GenericRepository()
        {
            this._context = new SupplierDbContext();
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public T Insert(T obj)
        {
            return table.Add(obj);
        }
        public void InsertRange(List<T> obj)
        {
            table.AddRange(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate);
        }

        public IEnumerable<T> DeleteAll(Expression<Func<T, bool>> predicate)
        {
            var result = table.Where(predicate);
            return table.RemoveRange(result);
        }
        void IGenericRepository<T>.Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate);
        }
        public IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate);
        }
        public IQueryable<T> GetAllQueryableInclude(Expression<Func<T, bool>> predicate, string path)
        {
            return table.Where(predicate).Include(path);
        }
        public IQueryable<T> GetTwoPathQueryableInclude(Expression<Func<T, bool>> predicate, string path, string path1)
        {
            return table.Where(predicate).Include(path).Include(path1);
        }
        public IQueryable<T> GetThreePathQueryableInclude(Expression<Func<T, bool>> predicate, string path, string path1, string path2)
        {
            return table.Where(predicate).Include(path).Include(path1).Include(path2);
        }
        public IQueryable<T> GetForthPathQueryableInclude(Expression<Func<T, bool>> predicate, string path, string path1, string path2, string path3)
        {
            return table.Where(predicate).Include(path).Include(path1).Include(path2).Include(path3);
        }
        public T GetByIdQueryable(Expression<Func<T, bool>> predicate)
        {
            return table.FirstOrDefault(predicate);
        }
        public T GetByIdQueryableInclude(Expression<Func<T, bool>> predicate, string path)
        {
            return table.Where(predicate).Include(path).FirstOrDefault();
        }
    }
}
