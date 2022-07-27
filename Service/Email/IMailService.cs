namespace Repository.Email
{
    public interface IMailService
    {
        public Task<Response> SendEmailAsync(MailParameters dto);
    }
}
