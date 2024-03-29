﻿using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IUnitOfWork<T> : IDisposable where T : BaseEntity
    {
        IUserRepository UserRepository { get; }
        IUserDirectionRepoitory AddressRepo { get; }
        IProductRepository ProductRepo { get; }
        IRepository<T> BaseRepo { get;  }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
