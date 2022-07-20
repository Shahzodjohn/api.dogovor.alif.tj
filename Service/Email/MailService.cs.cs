using ConvertApiDotNet;
using Entity.ReturnMessage;
using Entity.TransferObjects;
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

        public async Task<Response> SendEmailResetAsync(MailParameters dto)
        {
            var convertApi = new ConvertApi("S1alNMap0GwMC3zi");
            var convert = await convertApi.ConvertAsync("rtf", "pdf",
                new ConvertApiFileParam("File", dto.FilePath));
            var OutputPath = dto.FilePath.Replace(dto.FilePath.Split('/').ToList().LastOrDefault(), "").TrimEnd(new char[] { '/' });
            await convert.SaveFilesAsync(OutputPath);
            dto.FilePath = dto.FilePath.Replace("rtf", "pdf");

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_settings.Mail);
            email.To.Add(MailboxAddress.Parse(dto.toEmail));
            var builder = new BodyBuilder();
            
            if (dto.FilePath != null)
            {
                var filePath = dto.FilePath.Split('/').ToArray().LastOrDefault();
                if (dto.Subject == "")
                    email.Subject = filePath;
                builder.Attachments.Add(dto.FilePath);
            }
            else
                email.Subject = dto.Subject;
            email.Body = builder.ToMessageBody();
            builder.HtmlBody = dto.htmlBody;
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_settings.Mail, _settings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            return new Response { StatusCode = System.Net.HttpStatusCode.OK };

            //email.Subject = "Восстановление пароля";
            //string bodyContent =  @"<!DOCTYPE html>
            //                            <html>
            //                            <head>
            //                            <style>
            //                                p {
            //                                    font-size: 20px;
            //                                    margin-left : 30px;
            //                                }
            //                                a {
            //                                    font-weight : bold;
            //                                }
            //                            </style>
            //                            </head>
            //                            <body>
            //                            <p> Alif Bank </p>";
            //bodyContent += $"<p> <a> E-mail адрес: </a>  {model.clientname} </p> ";
            //bodyContent += $" <p> <a> Пароль:  </a> {model.phone}  </p> ";


            //bodyContent += $" <p> <a> Email:  </a> {model.email}</p> ";
            //bodyContent += $" <div> <p> <a> Wiadomość: </a> {model.message} </p> </div> </body> </html>";

        }
    }
}
