﻿using Domain.TransferObjects;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateCategory(CategoryDTO categoryDto)
        {
            var category = await _categoryAndSubCategoryServices.CreateCategory(categoryDto);
            if (category.StatusCode != System.Net.HttpStatusCode.OK)
                return BadRequest(category);
            return Ok(category.StatusCode);
        }
        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile(int SubCategoryId)
        {
            var subCategoryFile = await _categoryAndSubCategoryServices.GetSubCategoryFile(SubCategoryId);
            return subCategoryFile.StatusCode != System.Net.HttpStatusCode.OK ? NotFound(subCategoryFile) : Ok(subCategoryFile);
        }
        
        [HttpPost("ReceiveFinalText")]
        public async Task<IActionResult> ReceiveFinalText([FromForm] TextDTO textDto)
        {
            var path = (Path.Combine(_webHostEnvironment.WebRootPath, $"{ DateTime.Today.ToString("D") }"));
            var response = await _categoryAndSubCategoryServices.ReceiveFinalText(textDto, path);
            return response.StatusCode == System.Net.HttpStatusCode.OK ? Ok(response) : BadRequest(response);
        }
    }
}
