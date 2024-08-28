using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Infraestructure.Repositories
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly FakeRubikStoreContext _context;
        private IDbTransaction _transaction;
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

        public IOrderRepository OrderRepo => new OrderRepository(_context);

        public IDeliveryRepository DeliveryRepo => new DeliveryRepository(_context);

        public ICategoryRepository CategoryRepo => new CategoryRepository(_context);

        public async Task BeginTransactionAsync()
        {
            var dbCtxTransaction = await _context.Database.BeginTransactionAsync();
            _transaction = dbCtxTransaction.GetDbTransaction();
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }
        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

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
