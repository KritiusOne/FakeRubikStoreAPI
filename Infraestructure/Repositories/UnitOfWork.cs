using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly FakeRubikStoreContext _context;
        public UnitOfWork(FakeRubikStoreContext contx)
        {
            _context = contx;
        }

        public IUserRepository UserRepository => new UserRepository(_context);
        public IUserDirectionRepoitory AddressRepo => new UserDirectionRepository(_context);
        public IReviewRepository ReviewRepo => new ReviewRepository(_context);
        public IProductRepository ProductRepo => new ProductRepository(_context);
        public IRepository<State> StateRepo => new BaseRepository<State>(_context);
        public IRepository<T> BaseRepo => new BaseRepository<T>(_context);


        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
