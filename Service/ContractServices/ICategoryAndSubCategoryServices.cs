using Entity.ContractChoice;
using Entity.ReturnMessage;
using Entity.TransferObjects;
using Microsoft.AspNetCore.Http;

namespace Service.ContractServices
{
    public interface ICategoryAndSubCategoryServices
    {
        public Task<Response> CreateSubCategory(SubCategoryDTO dto, string path);
    }
}
