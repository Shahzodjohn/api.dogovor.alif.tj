using ConnectionProvider.Context;
using Entity.ContractChoice;
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

        public async Task<SubCategory> CreateSubCategory(SubCategory subCategory)
        {
            var subCtgr = new SubCategory
            {
                 SubCategoryName = subCategory.SubCategoryName,
                  CategoryId = subCategory.CategoryId,
                   SampleInstance = subCategory.SampleInstance,
            };
            await _context.SubCategory.AddAsync(subCtgr);
            await _context.SaveChangesAsync();
            return subCtgr;
        }
    }
}
