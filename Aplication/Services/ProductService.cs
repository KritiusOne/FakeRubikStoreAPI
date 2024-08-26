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
            var Prod = _unitOfWork.ProductRepo.GetAllWithTables();
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
        public Product GetById(int id)
        {
            return _unitOfWork.ProductRepo.GetByIdWithTables(id);
        }

        public async Task UpdateProduct(Stream thumbnailImg, Stream productImg, Product ProductInfo, string blobKey, int id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                var actualProductInfo = _unitOfWork.ProductRepo.GetByIdWithTables(id);
                
                if (actualProductInfo == null)
                {
                    throw new BaseException("Product not found");
                }
                actualProductInfo.Name = ProductInfo.Name;
                actualProductInfo.Price = ProductInfo.Price;
                actualProductInfo.Description = ProductInfo.Description;
                actualProductInfo.Stock = ProductInfo.Stock;

                var blobServices = new BlobServices();
                var nameThumbnail = await blobServices.UploadBlobAsync(thumbnailImg, Enums.AzureBlobTypes.Products, blobKey);
                if(nameThumbnail != null)
                {
                    actualProductInfo.Thumbnail = nameThumbnail;
                }
                var nameProductImg = await blobServices.UploadBlobAsync(thumbnailImg, Enums.AzureBlobTypes.Products, blobKey);
                if(nameProductImg != null)
                {
                    actualProductInfo.Image = nameProductImg;
                }

                foreach(var tag in ProductInfo.ProductCategories)
                {
                    var isTag = actualProductInfo.ProductCategories.Any(actualTag =>
                    actualTag.IdCategory == tag.IdCategory && actualTag.IdProduct == tag.IdProduct);
                    if (!isTag)
                    {
                        actualProductInfo.ProductCategories.Add(tag);
                    }
                }
                _unitOfWork.ProductRepo.Update(id, actualProductInfo);
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Problemas al realizar la actualización", ex);
            }
        }
    }
}
