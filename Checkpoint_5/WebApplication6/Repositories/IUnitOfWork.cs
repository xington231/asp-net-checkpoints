using WebApplication6.Data;
using WebApplication6.Models;

namespace WebApplication6.Repositories
{

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<Category> Categories { get; }
        Task<int> CompleteAsync();
    }


    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IRepository<Product> Products { get; }
        public IRepository<Category> Categories { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Products = new Repository<Product>(context);
            Categories = new Repository<Category>(context);
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
