using Entity.ContractChoice;
using Entity.TransferObjects;
using Microsoft.AspNetCore.Http;

namespace Repository
{
    public interface ICategoryAndSubCategoryRepository
    {
        public Task<SubCategory> CreateSubCategory(SubCategory subCategory);
        public Task<Category> CreateCategory(CategoryDTO dto);
        public Task<SubCategory> GetSubCategory(int Id);
        public Task<string> GetSubCategoryFile(int Id);
        
    }
}
