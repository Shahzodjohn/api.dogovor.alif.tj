namespace Domain.TransferObjects
{
    public class MailParameters
    {
        public string toEmail{ get; set; } = string.Empty;
        public string htmlBody { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;    
    }   
}
    