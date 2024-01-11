using Microsoft.EntityFrameworkCore;
using Squadmakers.Application.Interfaces;
using Squadmakers.Infraestructure.Models;

namespace Squadmakers.Application.Repositories
{
    public class RepositoryJoke<T> : IRepositoryJoke<T> where T : class
    {
        //The following variable is going to hold the EmployeeDBContext instance
        private readonly SquadmakersContext _context;

        //Initializing the EmployeeDBContext instance which it received as an argument
        public RepositoryJoke(SquadmakersContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                return true;
            }

            return false;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
