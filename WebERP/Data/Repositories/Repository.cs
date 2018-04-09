using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebERP.Models;

namespace WebERP.Data.Repositories
{
    public class GenericRepository<T> where T : class, IEntity  
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IQueryable<T> _setNotTrackable;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _setNotTrackable = context.Set<T>().AsNoTracking();
        }
        
        public void Save(T entity)
        {
            if (entity.Id == default(int))
                _dbSet.Add(entity);
            else
                _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }
        
        public void Delete(int id)
        {
            var entityToRemove = FindById(id);
            if (entityToRemove == null)
                return;

            _context.Entry(entityToRemove).State = EntityState.Deleted;
            _context.SaveChanges();
        }
        public T FindById(int id) => _setNotTrackable.FirstOrDefault(e => e.Id == id);

        public IEnumerable<T> List() => _setNotTrackable.ToList();
    }
}
