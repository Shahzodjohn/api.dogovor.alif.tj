namespace Service
{
    public class ArchiveService : IArchiveService
    {
        private readonly IArchiveRepository _archive;

        public ArchiveService(IArchiveRepository archive)
        {
            _archive = archive;
        }

        public async Task<string> ReturnFile(ReturnFileDTO fileDto, string path, User user)
        {
            try
            {
                var fileName = Path.Combine(path, fileDto.ContractName + ".rtf");
                
                var convertApi = new ConvertApi("S1alNMap0GwMC3zi");
                var convert = await convertApi.ConvertAsync("rtf", $"{fileDto.format}", new ConvertApiFileParam("File", fileName));
                await convert.SaveFilesAsync(path);
                fileName = fileName.Replace("rtf", fileDto.format);
                
                var directoryInfo = new DirectoryInfo(path);

                //FileInfo[] files = directoryInfo.GetFiles($"{fileDto.ContractName}.rtf")
                //                     .Where(p => p.Extension == ".rtf").ToArray();

                await _archive.ArchivePost(new ArchiveDTO { ContractName = fileDto.ContractName, ExecutorsEmail = user.EmailAddress, 
                                                            ExecutorsFullName = String.Concat(user.FirstName," ",user.LastName),
                                                            DocumentType = fileDto.DocumentName, FilePath = fileName });

                LogProvider.GetInstance().Info( new Response { StatusCode = System.Net.HttpStatusCode.OK }.ToString(), "Successfull process!");
                return fileName.Replace("rtf", fileDto.format);
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error(new Response { StatusCode = System.Net.HttpStatusCode.BadRequest }.ToString(), ex.Message.ToString());
                return null;
            }
        }   
    }
}
