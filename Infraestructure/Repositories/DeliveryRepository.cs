using Aplication.Entities;
using Aplication.Interfaces;
using Infraestructure.Data;

namespace Infraestructure.Repositories
{
    public class DeliveryRepository : BaseRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(FakeRubikStoreContext db) : base(db)
        {}

        public async Task<Delivery> CreateAndReturn(Delivery delivery)
        {
            await _context.AddAsync(delivery);
            await _context.SaveChangesAsync();
            return delivery;             
        }
    }
}
