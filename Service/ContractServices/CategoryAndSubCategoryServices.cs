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
                    new ConvertApiFileParam("File", filePath)
                );

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
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error("400", ex.Message.ToString());
            }
            return new Response { Status = "200", Message = "ok"};
        }
    }
}
