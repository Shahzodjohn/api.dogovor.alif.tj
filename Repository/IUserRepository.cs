using Entity;
using Entity.ReturnMessage;
using Entity.TransferObjects;
using Entity.User;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;

namespace Repository
{
    public interface IUserRepository
    {
        public Task<EntityEntry<User>> InsertUser(RegisterDTO dto);
        public Task<User> GetUserbyEmail(string email);
        public Task<Role> GetUserRole(int Id);
        public Task<string> JSONToken(User user);
    }
}
