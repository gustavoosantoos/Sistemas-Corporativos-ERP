using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebERP.Models;

namespace WebERP.Data.Repositories
{
    public abstract class BaseRepository<T> where T : class, IEntity  
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly IQueryable<T> _setNotTrackable;

        protected BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _setNotTrackable = context.Set<T>().AsNoTracking();
        }

        public virtual void Save(T entity)
        {
            if (entity.Id == default(int))
                _dbSet.Add(entity);
            else
                _context.Entry(entity).State = EntityState.Modified;

            _context.SaveChanges();
        }
        
        public virtual void Delete(int id)
        {
            var entityToRemove = FindById(id);
            if (entityToRemove == null)
                return;

            _context.Entry(entityToRemove).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public virtual T FindById(int id) => _setNotTrackable.FirstOrDefault(e => e.Id == id);

        protected IQueryable<T> All() => _setNotTrackable;
    }
}
