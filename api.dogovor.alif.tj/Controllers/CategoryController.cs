using ConnectionProvider.Context;
using Entity.ContractChoice;
using Entity.TransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.ContractServices;

namespace api.dogovor.alif.tj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryAndSubCategoryServices _categoryAndSubCategoryServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbСontext _context;

        public CategoryController(ICategoryAndSubCategoryServices categoryAndSubCategoryServices, IWebHostEnvironment webHostEnvironment, AppDbСontext context)
        {
            _categoryAndSubCategoryServices = categoryAndSubCategoryServices;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        [HttpPost("CreateSubCategory")]
        public async Task<IActionResult> CreateSubCategory([FromForm] SubCategoryDTO dto)
        {
            var path = (Path.Combine(_webHostEnvironment.WebRootPath, $"{ DateTime.Today.ToString("D") }"));
            var res = await _categoryAndSubCategoryServices.CreateSubCategory(dto, path);
            return Ok(res);
        }

        [HttpGet("GetSubcategory")]
        public async Task<IActionResult> GetSubcategory(int Id)
        {
            var subcategory = await _context.SubCategory.FindAsync(Id);
            return Ok(subcategory.SampleInstance);
        }

        [HttpPost("Create a category")]
        public async Task<IActionResult> CreateCategory(CategoryDTO dTO)
        {
            var category = new Category
            {
                CategoryName = dTO.CategoryName,
            };
            await _context.Category.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }
    }
}
