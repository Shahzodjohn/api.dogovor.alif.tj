﻿using Entity.ContractChoice;
using Entity.ReturnMessage;
using Entity.TransferObjects;
using Microsoft.AspNetCore.Http;

namespace Service.ContractServices
{
    public interface ICategoryAndSubCategoryServices
    {
        public Task<Response> CreateSubCategory(SubCategoryDTO dto, string path);
        public Task<Response> CreateCategory(CategoryDTO dto);
        public Task<Response> GetSubCategory(int Id);
        public Task<Response> GetSubCategoryFile(int Id);
    }
}
    