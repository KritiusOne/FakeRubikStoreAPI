using Aplication.CustomEntities;
using Aplication.Entities;
using Aplication.Enums;
using Aplication.Exceptions;
using Aplication.Interfaces;
using Aplication.QueryFilters;
using System.Collections.Generic;

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
            var Prod = filters.CategoriesIds == null ? _unitOfWork.ProductRepo.GetAllWithTables() : _unitOfWork.ProductRepo.GetAllWithTablesFilteredByCategories(filters.CategoriesIds);
            if (filters.MaxPrice != null)
            {
                Prod = Prod.Where(prod => prod.Price <= filters.MaxPrice);
            }
            if(filters.MinPrice != null)
            {
                Prod = Prod.Where(prod => prod.Price >= filters.MinPrice);
            }
            if(filters.NameProduct != null)
            {
                Prod = Prod.Where(prod => prod.Name.ToUpper().Contains(filters.NameProduct.ToUpper()));
            }
            if(filters.DescriptionProduct != null)
            {
                Prod = Prod.Where(prod => prod.Description.ToUpper().Contains(filters.DescriptionProduct.ToUpper()));
            }
            var paginationProducts = PagedList<Product>.CreatedPagedList(Prod, filters.PageNumber, filters.PageSize);
            return paginationProducts;
        }

        public async Task AddProduct(Product product, Stream ThumbnailImg, Stream ProductImg, string key)
        {
            if(product == null)
            {
                throw new BaseException("Bad request, Product is null");
            }
            try
            {
                var blobService = new BlobServices();
                string thumbnailName = await blobService.UploadBlobAsync(ThumbnailImg, AzureBlobTypes.Products, key);
                product.Thumbnail = thumbnailName;
                product.Image = await blobService.UploadBlobAsync(ProductImg, AzureBlobTypes.Products, key);
                await _unitOfWork.ProductRepo.Add(product);
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Hubo un problema al momento de crear el registro", ex);
            }
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
                var urlThumbnail = await blobServices.UploadBlobAsync(thumbnailImg, Enums.AzureBlobTypes.Products, blobKey);
                if(urlThumbnail != null)
                {
                    await blobServices.DeleteAsync(AzureBlobTypes.Products,actualProductInfo.Thumbnail, blobKey);
                    actualProductInfo.Thumbnail = urlThumbnail;
                }
                var urlProductImg = await blobServices.UploadBlobAsync(productImg, Enums.AzureBlobTypes.Products, blobKey);
                if(urlProductImg != null)
                {
                    await blobServices.DeleteAsync(AzureBlobTypes.Products, actualProductInfo.Image, blobKey);
                    actualProductInfo.Image = urlProductImg;
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
