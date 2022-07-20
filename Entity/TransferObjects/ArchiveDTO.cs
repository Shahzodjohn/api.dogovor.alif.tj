﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.TransferObjects
{
    public class ArchiveDTO
    {
        public string ContractName { get; set; }
        public string ExecutorsEmail { get; set; }
        public string ExecutorsFullName { get; set; }
        public string DocumentType { get; set; }
        public string FilePath { get; set; }
    }
}