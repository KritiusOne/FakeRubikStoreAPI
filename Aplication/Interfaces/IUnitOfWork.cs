﻿using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUnitOfWork<T> : IDisposable where T : class
    {
        IUserRepository UserRepository { get; }
        IUserDirectionRepoitory AddressRepo { get; }
        IProductRepository ProductRepo { get; }
        IReviewRepository ReviewRepo { get; }
        IOrderRepository OrderRepo { get; }
        IDeliveryRepository DeliveryRepo { get; }
        ICategoryRepository CategoryRepo { get; }
        IRepository<T> BaseRepo { get;  }
        Task BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
