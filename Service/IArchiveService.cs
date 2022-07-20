using Entity.TransferObjects;
using Entity.User;

namespace Service
{
    public interface IArchiveService
    {
        public Task<string> ReturnFile(ReturnFileDTO dto, string path, User user);
    }
}
    