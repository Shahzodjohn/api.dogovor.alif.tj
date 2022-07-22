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
        public Task<UserCode> GetUserCodeCompared(string email);
        public Task<UserCode> InsertUserCode(string randomNumber, int UserId, DateTime date); 
        public Task UpdateCode(UserCode userCode, int NewCode);
        public string GetUserByEmailAndCode(RandomNumberDTO dto);
        public Task<Response> UpdateUsersPassword(NewPasswordDTO dto);
    }
}
