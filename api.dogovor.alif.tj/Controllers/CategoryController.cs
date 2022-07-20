using ConnectionProvider.Context;
using ConvertApiDotNet;
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

        public CategoryController(ICategoryAndSubCategoryServices categoryAndSubCategoryServices, IWebHostEnvironment webHostEnvironment)
        {
            _categoryAndSubCategoryServices = categoryAndSubCategoryServices;
            _webHostEnvironment = webHostEnvironment;
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
            var subcategory = await _categoryAndSubCategoryServices.GetSubCategory(Id);
            if (subcategory.StatusCode != System.Net.HttpStatusCode.OK)
                return BadRequest(subcategory);
            return Ok(subcategory);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CategoryDTO dTO)
        {
            var category = await _categoryAndSubCategoryServices.CreateCategory(dTO);
            if (category.StatusCode != System.Net.HttpStatusCode.OK)
                return BadRequest(category);
            return Ok(category.StatusCode);
        }
        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile(int SubCategoryId)
        {
            var subCategoryFile = await _categoryAndSubCategoryServices.GetSubCategoryFile(SubCategoryId);
            if (subCategoryFile.StatusCode != System.Net.HttpStatusCode.OK)
                return NotFound();
            return Ok(subCategoryFile.Message);
        }
        
        [HttpPost("ReceiveFinalText")]
        public async Task<IActionResult> ReceiveFinalText([FromForm] TextDTO dto)
        {
            var path = (Path.Combine(_webHostEnvironment.WebRootPath, $"{ DateTime.Today.ToString("D") }"));
            var response = await _categoryAndSubCategoryServices.ReceiveFinalText(dto, path);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return Ok(response);
            return BadRequest(response);
        }
        

    }
}
