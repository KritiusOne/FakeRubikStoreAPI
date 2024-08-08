using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.Interfaces;
using Aplication.QueryFilters;

namespace Aplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork<Order> _unitOfWork;
        public OrderService(IUnitOfWork<Order> repo)
        {
            this._unitOfWork = repo;
        }


        public PagedList<Order> GetAll(OrderQueryFilters filters)
        {
            Console.WriteLine(filters.ToString());
            var response = _unitOfWork.OrderRepo.GetAll();
            if(filters.MinPrice != null)
            {
                response = response.Where(e => e.FinalPrice >= filters.MinPrice);
            }
            if(filters.MaxPrice != null)
            {
                response = response.Where(e => e.FinalPrice <= filters.MaxPrice);
            }
            if (filters.Date != null)
            {
                response = response.Where(e => e.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }
            if(filters.MinDate != null)
            {
                var MinDate = (DateTime)filters.MinDate;
                response = response.Where(e => DateTime.Compare(e.Date, MinDate) == 1 || DateTime.Compare(e.Date, MinDate) == 0 );
            }
            if(filters.MaxDate != null)
            {
                var MaxDate = (DateTime)filters.MaxDate;
                response = response.Where(e => DateTime.Compare(e.Date, MaxDate) == -1);
            }
            if(filters.IdUser != null)
            {
                response = response.Where(e => e.IdUser == filters.IdUser);
            }
            var AllOrders = PagedList<Order>.CreatedPagedList(response, filters.PageNumber, filters.PageSize);
            return AllOrders;
        }
        public async Task<Order> GetById(int id)
        {
            var Searched = await _unitOfWork.OrderRepo.GetByIdWithTables(id);
            return Searched;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            var newCode = Guid.NewGuid();
            var Envio = new Delivery()
            {
                IdState = 2,
                IdUser = order.IdUser,
                Code = newCode.ToString()
            };
            Envio = await _unitOfWork.DeliveryRepo.CreateAndReturn(Envio);
            order.IdDelivery = Envio.Id;
            await _unitOfWork.OrderRepo.AddAndReturn(order);
            var arrProducts = order.OrderProducts.ToList();
            for(int i = 0; i< arrProducts.Count; i++)
            {
                arrProducts[i].IdOrder = order.Id;
                var OrderProductComprobation = await _unitOfWork.OrderRepo.GetByIdOrderProdut(arrProducts[i].IdProduct, arrProducts[i].IdOrder);
                if (OrderProductComprobation == null)
                {
                    await _unitOfWork.OrderRepo.AddNewOrderProduct(arrProducts[i]);
                }
                
                if (i % 3 == 0)
                    await _unitOfWork.SaveChangesAsync();
            }
            await _unitOfWork.SaveChangesAsync();
            return order;
        }

    }
}
