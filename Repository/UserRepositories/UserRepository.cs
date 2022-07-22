using ConnectionProvider.Context;
using Entity;
using Entity.ReturnMessage;
using Entity.TransferObjects;
using Entity.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Repository
{
    public class UserRepository : IUserRepository   
    {
        private readonly AppDbСontext _сontext;
        private readonly IConfiguration _configuration;

        public UserRepository(AppDbСontext сontext, IConfiguration configuration)
        {
            _сontext = сontext;
            _configuration = configuration;
        }
        public async Task<EntityEntry<User>> InsertUser(RegisterDTO dto)
        {
            var register = await _сontext.Users.AddAsync(new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                EmailAddress = dto.EmailAddress.ToUpper(),
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = 1,//Id = 1(User)
            });
            await _сontext.SaveChangesAsync();
            return register;
        }

        public async Task<User> GetUserbyEmail(string email)
        {
            var user = await _сontext.Users.FirstOrDefaultAsync(x => x.EmailAddress == email.ToUpper());
            return user;
        }

        public async Task<string> JSONToken(User user)
        {
            var currentUser = await _сontext.Users.FirstOrDefaultAsync(x=>x.Id == user.Id);
            //var userRoles = userDepartment.Id;
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.EmailAddress),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };
            authClaims.Add(new Claim(ClaimTypes.Role, currentUser.RoleId.ToString()));
            var userIdentity = new ClaimsIdentity(authClaims, ClaimTypes.Name);
            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidateAudience"],
                expires: DateTime.Now.AddHours(8),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256));
            var Token = new JwtSecurityTokenHandler().WriteToken(token);
            return Token;
        }

        public async Task<Role> GetUserRole(int Id)
        {
            var user = await _сontext.Roles.FirstOrDefaultAsync(x => x.Id== Id);
            return user;
        }

        public async Task<UserCode> GetUserCodeCompared(string email)
        {
            var findUser = await GetUserbyEmail(email);
            if (findUser == null)
            return null;
            var UserCode = (from r in _сontext.UserCodes
                            join u in _сontext.Users on r.UserId equals u.Id
                            where r.UserId == findUser.Id
                            select r).FirstOrDefault();
            return UserCode;
        }
        public async Task<UserCode> InsertUserCode(string randomNumber, int UserId, DateTime date)
        {
            try
            {
                var dataInsert = new UserCode
                {
                    RandomNumber = randomNumber.ToString(),
                    UserId = UserId,
                    ValidDate = date
                };
                await _сontext.UserCodes.AddAsync(dataInsert);
                await _сontext.SaveChangesAsync();
                return dataInsert;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task UpdateCode(UserCode userCode, int NewCode)
        {
            userCode.RandomNumber = NewCode.ToString();
            await _сontext.SaveChangesAsync();
        }
        public string GetUserByEmailAndCode(RandomNumberDTO dto)
        {
            var UserEmail = (from r in  _сontext.UserCodes
                             join u in _сontext.Users on r.UserId equals u.Id
                             where u.EmailAddress.ToLower() == dto.Email.ToLower() && r.RandomNumber == dto.RandomNumber
                             select new { u.EmailAddress, r.RandomNumber }).FirstOrDefault();
            if (UserEmail == null)
                return null;
            return UserEmail.ToString();
        }

        public async Task<Response> UpdateUsersPassword(NewPasswordDTO dto)
        {
            var user = await GetUserbyEmail(dto.Email);
            try
            {
                if (user == null)
                    return new Response { StatusCode = System.Net.HttpStatusCode.NotFound };
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
                await _сontext.SaveChangesAsync();
                return new Response { StatusCode = System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
            }
        }
    }
}
