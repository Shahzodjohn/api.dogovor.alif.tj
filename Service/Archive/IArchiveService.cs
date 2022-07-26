using Domain.TransferObjects;
using Domain.User;

namespace Service
{
    public interface IArchiveService
    {
        public Task<string> ReturnFile(ReturnFileDTO dto, string path, User user);
    }
}
    