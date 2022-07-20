using Entity.ReturnMessage;
using Entity.TransferObjects;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
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

        public MailService(MailSettings settings)
        {
            _settings = settings;
        }

        public async Task<Response> SendEmailResetAsync(MailParameters dto)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_settings.Mail);
            email.To.Add(MailboxAddress.Parse(dto.toEmail));
            var builder = new BodyBuilder();
            if (dto.FilePath != null)
            {
                builder.Attachments.Add(dto.FilePath);
            }




            //byte[] filebytes;
            ////foreach (var file in Attechments)
            ////{   
            ////    if (file.Length > 0)
            ////    {
            //        using (var ms = new MemoryStream())
            //        {
            //            Attechments.CopyTo(ms);
            //            filebytes = ms.ToArray();
            //        }
            //        builder.Attachments.Add(Attechments.FileName, filebytes, ContentType.Parse(Attechments.ContentType));
            ////    }
            ////}

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
            builder.HtmlBody = dto.htmlBody;
            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_settings.Host, _settings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_settings.Mail, _settings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }

            //bodyContent += $" <p> <a> Email:  </a> {model.email}</p> ";
            //bodyContent += $" <div> <p> <a> Wiadomość: </a> {model.message} </p> </div> </body> </html>";
            return new Response { StatusCode = System.Net.HttpStatusCode.OK };
        }
    }
}
