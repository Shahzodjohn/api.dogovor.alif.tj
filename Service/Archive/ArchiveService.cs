using api.dogovor.alif.tj.LogSettings;
using ConvertApiDotNet;
using Entity.ReturnMessage;
using Entity.TransferObjects;
using Entity.User;
using Repository.ArchievumRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ArchiveService : IArchiveService
    {
        private readonly IArchiveRepository _archive;

        public ArchiveService(IArchiveRepository archive)
        {
            _archive = archive;
        }

        public async Task<string> ReturnFile(ReturnFileDTO dto, string path, User user)
        {
            try
            {
                var fileName = Path.Combine(path, dto.ContractName + ".rtf");
                
                var convertApi = new ConvertApi("S1alNMap0GwMC3zi");
                var convert = await convertApi.ConvertAsync("rtf", $"{dto.format}", new ConvertApiFileParam("File", fileName));
                await convert.SaveFilesAsync(path);
                fileName = fileName.Replace("rtf", dto.format);
                
                DirectoryInfo di = new DirectoryInfo(path);
                FileInfo[] files = di.GetFiles($"{dto.ContractName}.rtf")
                                     .Where(p => p.Extension == ".rtf").ToArray();

                ArchiveDTO archiveDTO = new ArchiveDTO()
                {
                    ContractName = dto.ContractName,
                    ExecutorsEmail = user.EmailAddress,
                    ExecutorsFullName = user.FirstName + " " + user.LastName,
                    DocumentType = dto.DocumentName,
                    FilePath = fileName,
                };
                await _archive.ArchivePost(archiveDTO);
                LogProvider.GetInstance().Info( new Response { StatusCode = System.Net.HttpStatusCode.OK }.ToString(), "Successfull process!");
                return fileName.Replace("rtf", dto.format);
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error(new Response { StatusCode = System.Net.HttpStatusCode.BadRequest }.ToString(), ex.Message.ToString());
                return null;
            }
            
        }   
    }
}
