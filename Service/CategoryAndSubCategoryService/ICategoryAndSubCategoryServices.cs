namespace Service.ContractServices
{
    public interface ICategoryAndSubCategoryServices
    {
        public Task<Response> CreateSubCategory(SubCategoryDTO dto, string path);
        public Task<Response> CreateCategory(CategoryDTO dto);
        public Task<Response> GetSubCategory(int Id);
        public Task<Response> GetSubCategoryFile(int Id);
        public Task<Response> ReceiveFinalText(TextDTO dto, string path);
    }
}
    