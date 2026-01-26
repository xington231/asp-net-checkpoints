using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;

namespace WebApplication6.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id); 
        Task CreateAsync(T entity); 
        Task UpdateAsync(T entity); 
        Task DeleteAsync(int id);
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context; 
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context) { _context = context; _dbSet = context.Set<T>(); }
        public async Task<IEnumerable<T>> GetAllAsync() { return await _dbSet.ToListAsync(); }
        public async Task<T> GetByIdAsync(int id) { return await _dbSet.FindAsync(id); }
        public async Task CreateAsync(T entity) { await _dbSet.AddAsync(entity); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(T entity) { _dbSet.Update(entity); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var entity = await _dbSet.FindAsync(id); _dbSet.Remove(entity); await _context.SaveChangesAsync(); }
    }
}


    
