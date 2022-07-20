using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.TransferObjects
{
    public class MailParameters
    {
        public string toEmail{ get; set; }
        public string htmlBody { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;    
    }   
}
    