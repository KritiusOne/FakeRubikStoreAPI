using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAllWithTables();
        Product GetByIdWithTables(int id);
    }
}
