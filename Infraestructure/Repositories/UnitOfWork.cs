using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FakeRubikStoreContext _context;
        public UnitOfWork(FakeRubikStoreContext contx) 
        {
            _context = contx;
        }

        public IRepository<User> UserRepository => new UserRepository(_context);

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
