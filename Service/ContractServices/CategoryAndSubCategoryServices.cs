using api.dogovor.alif.tj.LogSettings;
using ConvertApiDotNet;
using Entity.ContractChoice;
using Entity.ReturnMessage;
using Entity.TransferObjects;
using Microsoft.AspNetCore.Http;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ContractServices
{
    //LogProvider.GetInstance().Warning("400", "User not found!");
    public class CategoryAndSubCategoryServices : ICategoryAndSubCategoryServices
    {
        private readonly ICategoryAndSubCategoryRepository _subCategoryRepository;

        public CategoryAndSubCategoryServices(ICategoryAndSubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<Response> CreateCategory(CategoryDTO dto)
        {
            try
            {
                var category = await _subCategoryRepository.CreateCategory(dto);
                if(category == null)
                    return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest };
               return new Response { StatusCode = System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message  };
            }
        }

        public async Task<Response> CreateSubCategory(SubCategoryDTO dto, string path)
        {
            try
            {
                string filePath = Path.Combine(path, dto.form.FileName);
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    dto.form.CopyTo(fileStream);

                var convertApi = new ConvertApi("S1alNMap0GwMC3zi");
                var convert = await convertApi.ConvertAsync("docx", "rtf",
                    new ConvertApiFileParam("File", filePath));

                var rtfFile = filePath.Replace("docx", "rtf");
                var textFile = filePath.Replace("docx", "txt");
                await convert.SaveFilesAsync(path);
                if (!System.IO.File.Exists(textFile))
                    System.IO.File.Move(rtfFile, Path.ChangeExtension(rtfFile, ".txt"));
                else if (System.IO.File.Exists(textFile))
                    System.IO.File.Delete(textFile);
                var finaltext = System.IO.File.ReadAllText(textFile);
                var subCategory = new SubCategory
                {
                    SubCategoryName = dto.SubCategoryName,
                    SampleInstance = finaltext,
                    CategoryId = dto.CategoryId
                };
                var insertResponse = await _subCategoryRepository.CreateSubCategory(subCategory);
                System.IO.DirectoryInfo di = new DirectoryInfo(path);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                LogProvider.GetInstance().Info("200", "Successfull process!");
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error("400", ex.Message.ToString());
                return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
            return new Response { StatusCode = System.Net.HttpStatusCode.OK };
        }

        public async Task<Response> GetSubCategory(int Id)
        {
            try
            {
                var subcategory = await _subCategoryRepository.GetSubCategory(Id);
                LogProvider.GetInstance().Info("200", "Successfull process!");
                return new Response { StatusCode = System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error("400", ex.Message.ToString());
                return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
        }

        public async Task<Response> GetSubCategoryFile(int Id)
        {
            var fileString = await _subCategoryRepository.GetSubCategoryFile(Id);
            if(fileString == null)
            {
                LogProvider.GetInstance().Error("400", "File not found!");
                return new Response { StatusCode = System.Net.HttpStatusCode.NotFound, Message = "Sub Category not found! Is it hidin' Somewhere?" };
            }
            LogProvider.GetInstance().Info("200", "Successfull process!");
            return new Response { StatusCode = System.Net.HttpStatusCode.OK, Message = fileString };
        }
    }
}
