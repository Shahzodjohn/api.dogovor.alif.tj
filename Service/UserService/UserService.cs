using api.dogovor.alif.tj.LogSettings;
using Entity.ReturnMessage;
using Entity.TransferObjects;
using Repository;
using Repository.Email;
using System.Security.Claims;

namespace Service.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _uRepository;
        private readonly IMailService _mailService;

        public UserService(IUserRepository uRepository, IMailService mailService)
        {
            _uRepository = uRepository;
            _mailService = mailService;
        }
        public async Task<Response> RegisterUser(RegisterDTO dto)
        {
            try
            {
                var user = await _uRepository.GetUserbyEmail(dto.EmailAddress.ToUpper());
                if(user != null)
                    return new Response { StatusCode = System.Net.HttpStatusCode.Conflict, Message = "This accaunt already exists!" };
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
                {
                    string bodyContent = @$"<!DOCTYPE html>
<html lang='en'>
<head>
  <meta charset='UTF-8'>
  <meta http-equiv='X-UA-Compatible' content='IE=edge'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>Пиьмо</title>
  <link rel='preconnect' href='https://fonts.googleapis.com'>
  <link rel='preconnect' href='https://fonts.gstatic.com' crossorigin>
  <link href='https://fonts.googleapis.com/css2?family=Inter:wght@100;200;300;400;500;600;700;800;900&family=Montserrat:wght@200;300;400;500;600;700;800&family=PT+Sans:ital,wght@0,400;0,700;1,400;1,700&display=swap' rel='stylesheet'>
</head>
<body>
  
  <table border='0' cellpadding='0' cellspacing='0' style='max-width: 600px; margin: 0 auto; font-family: Montserrat, sans-serif; padding: 0' width='100%'>
    <tbody>
      <tr>
        <td style='text-align: center; padding: 30px 0; font-size: 2.5rem;'>
          <span style='color: #39B980; font-weight: 700;'>Alif Dogovor</span>
        </td>
      </tr>
      <tr>
        <td style='text-align: center; color: #272727; padding: 0 0 10px 0;'>
          <h1 style='font-weight: 500; letter-spacing: 0.03rem; margin: 20px 0 0 0;'>
            Здравствуйте, {dto.FirstName + " " + dto.LastName}
          </h1>
        </td>
      </tr>
      <tr>
        <td style='text-align: center; color: #272727; padding: 0 0 30px 0;'>
          <h3 style='font-weight: 500; letter-spacing: 0.03rem;'>
            Ваши данные для входа в аккаунт: 
          </h3>
        </td>
      </tr>
      <tr style='display: flex; justify-content: center; align-self: center;'>
        <td style='background-color: #f1f5f3; width: 100%; text-align: center; padding: 30px 0; font-weight: 900; font-size: 1.75rem; letter-spacing: 0.2rem; color: #20b472 !important;'>
          Ваш логин: {dto.EmailAddress}
        </td>
      </tr>
      <tr style='display: flex; justify-content: center; align-self: center;'>
        <td style='background-color: #f1f5f3; width: 100%; text-align: center; padding: 30px 0; font-weight: 900; font-size: 1.75rem; letter-spacing: 0.2rem; color: #20b472 !important;'>
          Ваш пароль: {dto.Password}
        </td>
      </tr>
      <tr style='border-collapse:collapse;margin:0;padding:0'>
        <td height='78' style='border-collapse:collapse;margin:0;padding:0'>&nbsp;</td>
      </tr>
      <tr style='border-collapse:collapse;margin:0;padding:0'>
        <td align='center' style='border-collapse:collapse;margin:0;padding:0'>
          <p style='color:#b1b1b1;font-weight:600;display:inline;font-size:15px;line-height:24px;margin:0;padding:0'>
            © 2022 Alif Tex
          </p>
        </td>
      </tr>
    </tbody>
  </table>
</body>
</html>";
                    var mailParams = new MailParameters
                    {
                        htmlBody = bodyContent,
                        toEmail = dto.EmailAddress,
                        Subject = "Параметры для входа в аккаунт"
                    };
                    var message = await _mailService.SendEmailAsync(mailParams);
                    await _uRepository.InsertUser(dto);
                }
                    
                return new Response { StatusCode = System.Net.HttpStatusCode.OK, Message = "Success!" };
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error(ex.Message.ToString());
                return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
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
                    return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "This User does not exists!" };
                }
                if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                {
                    LogProvider.GetInstance().Warning("400, Invalid credentials");
                    return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = "Invalid credentials" };
                }
                var tokenJWT = await _uRepository.JSONToken(user);
                return new Response { StatusCode = System.Net.HttpStatusCode.OK, Message = tokenJWT.ToString() };
            }
            catch (Exception ex)
            {
                LogProvider.GetInstance().Error(ex.Message.ToString());
                return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message };
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
