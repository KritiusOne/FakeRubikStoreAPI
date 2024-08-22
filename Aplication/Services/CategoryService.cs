using Aplication.Entities;
using Aplication.Exceptions;
using Aplication.Interfaces;

namespace Aplication.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork<Category> _unitOfWork;
        public CategoryService(IUnitOfWork<Category> _unitOfWork)
        {
            this._unitOfWork = _unitOfWork;
        }

        public async Task CreateCategory(Category newCategory)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var Ids = newCategory.ProductCategories
                    .GroupBy(nCat => nCat.IdProduct)
                    .Select(group => group.First().IdProduct)
                    .ToList();
                var existAll = _unitOfWork.CategoryRepo.ExistAllProducts(Ids);
                if (existAll)
                {
                    await _unitOfWork.CategoryRepo.Add(newCategory);
                    await _unitOfWork.SaveChangesAsync();
                    _unitOfWork.CommitTransaction();
                }
                else
                {
                    throw new BaseException("Alguno de los elementos no existe");
                }
            }catch(Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Problemas con la transaccion", ex);
            }
        }

        public async Task CreateManyProductsCategories(ICollection<ProductCategory> productsCategories)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();
                List<ProductCategory> filtered = productsCategories
                .GroupBy(pc => pc.IdProduct)
                .Select(pc => pc.First())
                .ToList();
                var IdsProducts = new List<int>();
                var IdsCategories = new List<int>();
                foreach (ProductCategory productCategory in filtered)
                {
                    if (!IdsCategories.Contains(productCategory.IdCategory))
                    {
                        IdsCategories.Add(productCategory.IdCategory);
                    }
                    IdsProducts.Add(productCategory.IdProduct);
                }
                var allPrdExist = _unitOfWork.CategoryRepo.ExistAllProducts(IdsProducts);
                var allCtgExist = _unitOfWork.CategoryRepo.ExistAllCategories(IdsCategories);
                if (!allPrdExist || !allCtgExist)
                {
                    string msg = allCtgExist ? "Algunos de los productos no existen" : "Alguna de las categorias no existen";
                    throw new BaseException(msg);
                }
                foreach(var pc in filtered)
                {
                    await _unitOfWork.CategoryRepo.CreateProductCategory(pc);
                    if (filtered.IndexOf(pc) % 3 == 0)
                        await _unitOfWork.SaveChangesAsync();
                }
                await _unitOfWork.SaveChangesAsync();
                _unitOfWork.CommitTransaction();
            }catch(Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception("Error en la transaccion", ex);
            }
        }
    }
}
