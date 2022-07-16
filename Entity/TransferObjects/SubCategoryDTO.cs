using Entity.ContractChoice;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.TransferObjects
{
    public class SubCategoryDTO
    {
        public IFormFile form { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
