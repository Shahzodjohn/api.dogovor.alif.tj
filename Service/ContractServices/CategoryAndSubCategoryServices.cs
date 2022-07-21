using api.dogovor.alif.tj.LogSettings;
using ConvertApiDotNet;
using Entity.ContractChoice;
using Entity.Entities.Archivievum;
using Entity.ReturnMessage;
using Entity.TransferObjects;
using Microsoft.AspNetCore.Http;
using Repository;
using Repository.ArchievumRepositories;
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
                await convert.SaveFilesAsync(path); 

                var rtfFile = filePath.Replace("docx", "rtf");
                var textFile = filePath.Replace("docx", "txt");
                
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
                await _subCategoryRepository.CreateSubCategory(subCategory);
                System.IO.DirectoryInfo directory = new DirectoryInfo(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in directory.GetDirectories())
                {
                    dir.Delete(true);
                }
                LogProvider.GetInstance().Info(System.Net.HttpStatusCode.OK.ToString(), "Successfull process!");
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error(System.Net.HttpStatusCode.BadRequest.ToString(), ex.Message.ToString());
                return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
            return new Response { StatusCode = System.Net.HttpStatusCode.OK };
        }   

        public async Task<Response> GetSubCategory(int Id)
        {
            try
            {
                var subcategory = await _subCategoryRepository.GetSubCategory(Id);
                if (subcategory != null)
                {
                    LogProvider.GetInstance().Info(System.Net.HttpStatusCode.OK.ToString(), "Successfull process!");
                    return new Response { StatusCode = System.Net.HttpStatusCode.OK };
                }
                else return new Response { StatusCode = System.Net.HttpStatusCode.NotFound };
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error(System.Net.HttpStatusCode.BadRequest.ToString(), ex.Message.ToString());
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
            LogProvider.GetInstance().Info(System.Net.HttpStatusCode.OK.ToString(), "Successfull process!");
            return new Response { StatusCode = System.Net.HttpStatusCode.OK, Message = fileString };
        }

        public async Task<Response> ReceiveFinalText(TextDTO dto, string path)
        {
            try
            {
                if (!System.IO.Directory.Exists(path))  
                    System.IO.Directory.CreateDirectory(path);

                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] files = di.GetFiles($"{dto.ContractName}.rtf")
                                     .Where(p => p.Extension == ".rtf").ToArray();
                foreach (FileInfo file in files)
                    try
                    {
                        file.Attributes = FileAttributes.Normal;
                        System.IO.File.Delete(file.FullName);
                    }
                    catch 
                    {
                        LogProvider.GetInstance().Error(System.Net.HttpStatusCode.BadRequest.ToString());
                        return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest };
                    }

                var fileName = Path.Combine(path, $"{dto.ContractName}.txt");
                if (System.IO.File.Exists(fileName))
                    System.IO.File.Delete(fileName);

                FileStream fs = System.IO.File.Create(fileName);
                    fs.Dispose();

                using (StreamWriter writer = System.IO.File.AppendText(fileName))
                    writer.WriteLine(dto.RTFtext);

                System.IO.File.Move(fileName, Path.ChangeExtension(fileName, ".rtf"));
                LogProvider.GetInstance().Info(System.Net.HttpStatusCode.OK.ToString(), "Successfull process!");
                return new Response { StatusCode = System.Net.HttpStatusCode.OK, Message = fileName.Replace("txt","rtf") };
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error(System.Net.HttpStatusCode.BadRequest.ToString());
                return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
        }
    }
}
