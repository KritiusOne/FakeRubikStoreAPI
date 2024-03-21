using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.Exceptions;
using Aplication.Interfaces;

namespace Aplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork<Product> _unitOfWork;
        public ProductService(IUnitOfWork<Product> unit)
        {
            this._unitOfWork = unit;
        }

        public PagedList<Product> GetAllProducts()
        {
            var Prod = _unitOfWork.ProductRepo.GetAll();
            var paginationProducts = PagedList<Product>.CreatedPagedList(Prod, 1, 20);
            return paginationProducts;
        }

        public async Task AddProduct(Product product)
        {
            if(product == null)
            {
                throw new BaseException("Bad request, Product is null");
            }
            await _unitOfWork.ProductRepo.Add(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
