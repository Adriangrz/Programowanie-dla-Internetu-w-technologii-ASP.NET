using Microsoft.EntityFrameworkCore;
using NetworkOfShops.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkOfShops.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private AplicationDbContext _context;
        private DbSet<T> _dbSet;

        public GenericRepository(AplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            IQueryable<T> query = _dbSet;

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByID(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task Delete(object id)
        {
            T entityToDelete = await _dbSet.FindAsync(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
