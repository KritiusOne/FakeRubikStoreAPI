using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.Enums;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.QueryFilters;
using System.Text.Json;

namespace Aplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork<Order> _unitOfWork;
        private readonly IFactusServices _factus;
        public OrderService(IUnitOfWork<Order> repo, IFactusServices factus)
        {
            this._unitOfWork = repo;
            _factus = factus;
        }


        public PagedList<Order> GetAll(OrderQueryFilters filters)
        {
            Console.WriteLine(filters.ToString());
            var response = _unitOfWork.OrderRepo.GetAllWithTables();
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
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var filtered = order.OrderProducts
                    .GroupBy(op => op.IdProduct)
                    .Select(group => group.First())
                    .ToList();
                var newCode = Guid.NewGuid();
                var Envio = new Delivery()
                {
                    IdState = (int)StatesTypes.NO_ADMITIDO,
                    IdUser = order.IdUser,
                    Code = newCode.ToString()
                };
                Envio = await _unitOfWork.DeliveryRepo.CreateAndReturn(Envio);
                order.IdDelivery = Envio.Id;

                order = await _unitOfWork.OrderRepo.AddAndReturn(order);

                foreach(var op in filtered)
                {
                    var ProductForEdit = await _unitOfWork.ProductRepo.GetById(op.IdProduct);
                    if(ProductForEdit == null)
                    {
                        throw new BaseException($"NO estoy encontrando el producto ${op.IdProduct}");
                    }
                    else
                    {
                        ProductForEdit.Stock -= op.ProductsNumber;
                        _unitOfWork.ProductRepo.Attach(ProductForEdit);
                        _unitOfWork.ProductRepo.Update(ProductForEdit.Id, ProductForEdit);
                    }

                    if(filtered.IndexOf(op) % 3 == 0)
                    {
                        await _unitOfWork.SaveChangesAsync();
                    }
                }
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return order;
            }
            catch(Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Exception al momento de realizar las operaciones", ex);
            }
            
        }

        public async Task<Order> CreateOrderWithFactus(Order order, string url, string CC, string token)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {

                var UserInfo = _unitOfWork.UserRepository.UserWithInfo(order.IdUser);
                _factus.SetURL(url);
                var ProductsItems = new List<FactusItem>();
                List<int> Ids = new List<int>();
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                foreach(var item in order.OrderProducts)
                {
                    Ids.Add(item.IdProduct);
                }
                var ProductsInfo = _unitOfWork.ProductRepo.GetAllProductsByIds(Ids);
                for(int i = 0; i < order.OrderProducts.Count; i++)
                {
                    FactusItem newItem = new FactusItem(ProductsInfo.ElementAt(i).Name,
                        order.OrderProducts.ElementAt(i).ProductsNumber,
                        order.OrderProducts.ElementAt(i).IdProduct.ToString(),
                        ProductsInfo.ElementAt(i).Price);
                    ProductsItems.Add(newItem);
                }
                string newIDMethod = UserInfo.InfoCard == null ? "48" : UserInfo.InfoCard.IdCardType.ToString();
                var factusBill = new FactusBill(newIDMethod, ProductsItems);
                factusBill.customer = new ClientFactus(CC,
                    UserInfo.Name + " " + UserInfo.SecondName,
                    UserInfo.Email,
                    UserInfo.Phone,
                    "2",
                    UserInfo.AdressInfo.IdCity.ToString());
               factusBill.items = ProductsItems;
                _factus.SetURL(url);
                var JsonFactus = JsonSerializer.Serialize(factusBill);
                var res = await _factus.BillCreate(token, JsonFactus, "Bearer");
                Console.WriteLine(res);
                _unitOfWork.CommitTransaction();
                return order;
            }
            catch(Exception e)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Error on bill creaton", e);
            }
        }
    }
}
