using Aplication.CustomEntities;
using Aplication.CustomEntities.ExternalsClass;
using Aplication.Entities;
using Aplication.Enums;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.QueryFilters;
using System;
using System.Text.Json;

namespace Aplication.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork<Order> _unitOfWork;
        private readonly IFactusServices _factus;
        private JsonSerializerOptions SerializeOptions;
        public OrderService(IUnitOfWork<Order> repo, IFactusServices factus)
        {
            this._unitOfWork = repo;
            _factus = factus;
            SerializeOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = new LowerCaseUnderscoreNamingPolicy(),
                WriteIndented = true
            };
        }


        public PagedList<Order> GetAll(OrderQueryFilters filters)
        {
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
        public async Task<Order> CreateWithBasic(Order order)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                order = await CreateNewOrder(order);
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

        public async Task<Order> CreateOrderWithFactus(Order order, (string, string) Urls, string CC, string token)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                _factus.SetURL(Urls.Item1);
                List<int> Ids = new List<int>();
                foreach(var item in order.OrderProducts)
                {
                    Ids.Add(item.IdProduct);
                }
                var factusBill = BillCreateFactus(order.IdUser, Ids, order, CC);
                var JsonFactus = JsonSerializer.Serialize(factusBill);
                var res = await _factus.BillCreate(token, JsonFactus, "Bearer");
                var Bill = JsonSerializer.Deserialize<BillCreateResponse>(res, SerializeOptions);
                _factus.SetURL(Urls.Item2);
                string validateBillResponse = await _factus.BillValidate(Bill.Data.Bill.Number, token, "Bearer");
                var BillValidated = JsonSerializer.Deserialize<BillCreateResponse>(validateBillResponse, SerializeOptions);
                if (BillValidated == null) throw new BaseException("Error on validate bill");
                order.BillDian = BillValidated.Data.Bill.Qr;
                var newOrder = await CreateNewOrder(order);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
                return newOrder;
            }
            catch(Exception e)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Error on bill creaton", e);
            }
        }
        private FactusBill BillCreateFactus(int UserID, List<int> Ids, Order order, string CC)
        {
            var UserInfo = _unitOfWork.UserRepository.UserWithInfo(UserID);
            var ProductsItems = new List<FactusItem>();
            var ProductsInfo = _unitOfWork.ProductRepo.GetAllProductsByIds(Ids);
            for (int i = 0; i < order.OrderProducts.Count; i++)
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

            return factusBill;
        }
        private async Task<Order> CreateNewOrder(Order order)
        {
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

            foreach (var op in filtered)
            {
                var ProductForEdit = await _unitOfWork.ProductRepo.GetById(op.IdProduct);
                if (ProductForEdit == null)
                {
                    throw new BaseException($"NO estoy encontrando el producto ${op.IdProduct}");
                }
                ProductForEdit.Stock -= op.ProductsNumber;
                _unitOfWork.ProductRepo.Attach(ProductForEdit);
                _unitOfWork.ProductRepo.Update(ProductForEdit.Id, ProductForEdit);

                if (filtered.IndexOf(op) % 3 == 0)
                {
                    await _unitOfWork.SaveChangesAsync();
                }
            }
            return order;
        }
    }
}
