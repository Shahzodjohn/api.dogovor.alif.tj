using ConnectionProvider.Context;
using Entity.ContractChoice;
using Entity.TransferObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CategoryAndSubCategoryRepository : ICategoryAndSubCategoryRepository
    {
        private readonly AppDbСontext _context;

        public CategoryAndSubCategoryRepository(AppDbСontext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(CategoryDTO dto)
        {
            var category = new Category
            {
                CategoryName = dto.CategoryName,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<SubCategory> CreateSubCategory(SubCategory subCategory)
        {
            var subCtgr = new SubCategory
            {
                 SubCategoryName = subCategory.SubCategoryName,
                  CategoryId = subCategory.CategoryId,
                   SampleInstance = subCategory.SampleInstance,
            };
            await _context.SubCategories.AddAsync(subCtgr);
            await _context.SaveChangesAsync();
            return subCtgr;
        }

        public async Task<SubCategory> GetSubCategory(int Id)
        {
            var subcategory = await _context.SubCategories.FindAsync(Id);
            if (subcategory != null)
                return subcategory;
            else return null;
        }

        public async Task<string> GetSubCategoryFile(int Id)
        {
            var FileString = await _context.SubCategories.FindAsync(Id);
            if (FileString != null)
                return FileString.SampleInstance;
            else return null;
        }
    }
}
