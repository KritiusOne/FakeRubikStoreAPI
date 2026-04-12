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
        private readonly IFileStorageService _fileStorageService;
        public ProductService(IUnitOfWork<Product> unit, IFileStorageService fileStorageService)
        {
            this._unitOfWork = unit;
            this._fileStorageService = fileStorageService;
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

        public async Task AddProduct(Product product, Stream ThumbnailImg, Stream ProductImg)
        {
            if(product == null)
            {
                throw new BaseException("Bad request, Product is null");
            }
            string? thumbnailName = null;
            string? productImageName = null;
            try
            {
                thumbnailName = await _fileStorageService.UploadFileAsync(ThumbnailImg, StorageContainers.Products, Guid.NewGuid().ToString());
                product.Thumbnail = thumbnailName;
                productImageName = await _fileStorageService.UploadFileAsync(ProductImg, StorageContainers.Products, Guid.NewGuid().ToString());
                product.Image = productImageName;
                await _unitOfWork.ProductRepo.Add(product);
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                if (!string.IsNullOrWhiteSpace(thumbnailName))
                {
                    try
                    {
                        await _fileStorageService.DeleteFileAsync(StorageContainers.Products, thumbnailName);
                    }
                    catch
                    {
                    }
                }

                if (!string.IsNullOrWhiteSpace(productImageName))
                {
                    try
                    {
                        await _fileStorageService.DeleteFileAsync(StorageContainers.Products, productImageName);
                    }
                    catch
                    {
                    }
                }

                throw new Exception("Hubo un problema al momento de crear el registro", ex);
            }
        }
        public Product GetById(int id)
        {
            return _unitOfWork.ProductRepo.GetByIdWithTables(id);
        }

        public async Task UpdateProduct(Stream thumbnailImg, Stream productImg, Product ProductInfo, int id)
        {
            string? previousThumbnail = null;
            string? previousImage = null;
            string? newThumbnail = null;
            string? newImage = null;

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
                previousThumbnail = actualProductInfo.Thumbnail;
                previousImage = actualProductInfo.Image;

                newThumbnail = await _fileStorageService.UploadFileAsync(thumbnailImg, StorageContainers.Products, Guid.NewGuid().ToString());
                if(!string.IsNullOrWhiteSpace(newThumbnail))
                {
                    actualProductInfo.Thumbnail = newThumbnail;
                }
                newImage = await _fileStorageService.UploadFileAsync(productImg, StorageContainers.Products, Guid.NewGuid().ToString());
                if(!string.IsNullOrWhiteSpace(newImage))
                {
                    actualProductInfo.Image = newImage;
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

                if (!string.IsNullOrWhiteSpace(previousThumbnail) && previousThumbnail != newThumbnail)
                {
                    try
                    {
                        await _fileStorageService.DeleteFileAsync(StorageContainers.Products, previousThumbnail);
                    }
                    catch
                    {
                    }
                }

                if (!string.IsNullOrWhiteSpace(previousImage) && previousImage != newImage)
                {
                    try
                    {
                        await _fileStorageService.DeleteFileAsync(StorageContainers.Products, previousImage);
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();

                if (!string.IsNullOrWhiteSpace(newThumbnail))
                {
                    try
                    {
                        await _fileStorageService.DeleteFileAsync(StorageContainers.Products, newThumbnail);
                    }
                    catch
                    {
                    }
                }

                if (!string.IsNullOrWhiteSpace(newImage))
                {
                    try
                    {
                        await _fileStorageService.DeleteFileAsync(StorageContainers.Products, newImage);
                    }
                    catch
                    {
                    }
                }

                throw new Exception("Problemas al realizar la actualización", ex);
            }
        }
    }
}
