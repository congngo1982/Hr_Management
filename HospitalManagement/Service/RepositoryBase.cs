using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class RepositoryBase<T> where T : class
    {
        private readonly HospitalManagementDBContext _dbContext;
        private DbSet<T> _dbSet;

        public RepositoryBase(HospitalManagementDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
            _dbContext.SaveChanges();
        }
        public void Delete(T item)
        {
            _dbSet.Remove(item);
            _dbContext.SaveChanges();
        }
        public void Update(T item)
        {
            var tracker = _dbContext.Attach(item);
            tracker.State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
        public T GetById(object id)
        {
            return _dbSet.Find(id);
        }

    }
}
