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
    }
}
