using api.dogovor.alif.tj.LogSettings;
using Entity.ReturnMessage;
using Entity.TransferObjects;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _uRepository;

        public UserService(IUserRepository uRepository)
        {
            _uRepository = uRepository;
        }
        public async Task<Response> RegisterUser(RegisterDTO dto)
        {
            try
            {
                if (dto.Password != dto.RepeatPassword)
                {
                    LogProvider.GetInstance().Warning("400", "User not found!");
                    return new Response { Status = "400", Message = "The password doesn't match with repeated one!" };
                }
                if (/*!*/dto.EmailAddress.Contains("@team.alif.tj"))
                {
                    LogProvider.GetInstance().Warning("Error while adding a user, please enter a valid email address!");
                    return new Response { Status = "400", Message = "Error while adding a user, please enter a valid email address!" };
                }
                else
                    await _uRepository.InsertUser(dto);
                return new Response { Status = "200", Message = "Success!" };
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error(ex.Message.ToString());
                return new Response { Status = "400", Message = ex.Message };
            }
        }
        public async Task<Response> Login(AuthorizationDTO dto)
        {
            var user = await _uRepository.GetUserbyEmail(dto.EmailAddress);
            try
            {
                if (user == null)
                {
                    LogProvider.GetInstance().Warning("400, This User does not exists!");
                    return new Response { Status = "400", Message = "This User does not exists!" };
                }
                if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                {
                    LogProvider.GetInstance().Warning("400, Invalid credentials");
                    return new Response { Status = "400", Message = "Invalid credentials" };
                }
                var tokenJWT = await _uRepository.JSONToken(user);
                return new Response { Status = "200", Message = tokenJWT.ToString() };
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error(ex.Message.ToString());
                return new Response { Status = "400", Message = ex.Message };
            }
        }

        public async Task<Entity.User.User> UsersInformation(ClaimsIdentity claim)
        {
            var user = await _uRepository.GetUserbyEmail(claim.Name);
            var role = await _uRepository.GetUserRole(user.RoleId);

            var userInfo = new Entity.User.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailAddress = user.EmailAddress,
                RoleId = user.RoleId,
                Role = role
            };
            return userInfo;

        }
    }
}
