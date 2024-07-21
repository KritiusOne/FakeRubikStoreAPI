using Aplication.Entities;

namespace Aplication.Interfaces
{
    public interface IDeliveryRepository : IRepository<Delivery>
    {
        Task<Delivery> CreateAndReturn(Delivery delivery);
    }
}
