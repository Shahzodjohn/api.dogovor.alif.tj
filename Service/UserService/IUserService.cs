using Entity.ReturnMessage;
using Entity.TransferObjects;
using Entity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService
{
    public interface IUserService
    {
        public Task<Response> RegisterUser(RegisterDTO dto);
        public Task<Response> Login(AuthorizationDTO dto);
        public Task<User> UsersInformation(ClaimsIdentity claim);
    }
}
