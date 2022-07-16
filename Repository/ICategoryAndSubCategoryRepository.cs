using Entity.ContractChoice;
using Microsoft.AspNetCore.Http;

namespace Repository
{
    public interface ICategoryAndSubCategoryRepository
    {
        public Task<SubCategory> CreateSubCategory(SubCategory subCategory);
    }
}
