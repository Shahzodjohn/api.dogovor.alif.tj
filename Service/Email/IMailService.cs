using Entity.ReturnMessage;
using Entity.TransferObjects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Email
{
    public interface IMailService
    {
        public Task<Response> SendEmailAsync(MailParameters dto);
    }
}
