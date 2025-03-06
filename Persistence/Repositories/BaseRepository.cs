using FinalProjectBakary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary.Persistence.Repositories
{
    public class BaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            var response = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var persistenceEntities = await _context.Set<T>().ToListAsync();
            return persistenceEntities;
        }
        public async Task SaveMany(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            var result = await _context.SaveChangesAsync();
        }
        public async Task SaveContextChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllPendingAsync()
        {
            var persistenceEntities = await _context.Set<T>().ToListAsync();
            return persistenceEntities;
        }

        public async Task<IEnumerable<T>> GetAllPendingAsync()
        {
            var persistenceEntities = await _context.Set<T>().ToListAsync();
            return persistenceEntities;
        }
        
    }
}
