using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.QueryFilters;

namespace Aplication.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork<Product> _unitOfWork;
        public ProductService(IUnitOfWork<Product> unit)
        {
            this._unitOfWork = unit;
        }

        public PagedList<Product> GetAllProducts(ProductQueryFilter filters)
        {
            var Prod = _unitOfWork.ProductRepo.GetAll();
            var paginationProducts = PagedList<Product>.CreatedPagedList(Prod, filters.PageNumber, filters.PageSize);
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
        public async Task<Product> GetById(int id)
        {
            return await _unitOfWork.ProductRepo.GetById(id);
        }
    }
}
