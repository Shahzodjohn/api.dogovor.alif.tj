using ConvertApiDotNet;
using Domain.ReturnMessage;
using Domain.TransferObjects;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Email
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;

        public MailService(IOptions<MailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<Response> SendEmailAsync(MailParameters dto)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_settings.Mail);
                email.To.Add(MailboxAddress.Parse(dto.toEmail));
                var builder = new BodyBuilder();

                if (dto.FilePath != "" && dto.FilePath != null)
                {
                    var convertApi = new ConvertApi("S1alNMap0GwMC3zi");
                    var convert = await convertApi.ConvertAsync("rtf", "pdf",
                        new ConvertApiFileParam("File", dto.FilePath));

                    var OutputPath = dto.FilePath.Replace(dto.FilePath.Split('/').ToList().LastOrDefault(), "").TrimEnd(new char[] { '/' });
                    await convert.SaveFilesAsync(OutputPath);
                    dto.FilePath = dto.FilePath.Replace("rtf", "pdf");
                    var filePath = dto.FilePath.Split('/').ToArray().LastOrDefault();
                    if (dto.Subject == "")
                        email.Subject = filePath;
                    DirectoryInfo directory = new DirectoryInfo(OutputPath);
                    foreach (FileInfo file in directory.GetFiles(filePath))
                    {
                        if (file.Exists)
                        {
                            builder.Attachments.Add(file.FullName);
                        }
                    }
                }
                else
                    email.Subject = dto.Subject;
                builder.HtmlBody = dto.htmlBody;
                email.Body = builder.ToMessageBody();
                using (var smtp = new SmtpClient())
                {
                    smtp.Connect(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                    smtp.Authenticate(_settings.Mail, _settings.Password);
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                }
                return new Response { StatusCode = System.Net.HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new Response { StatusCode = System.Net.HttpStatusCode.BadRequest, Message = ex.Message.ToString() };
            }
        }
    }
}
