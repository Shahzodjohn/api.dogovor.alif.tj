using Entity.Entities.Archivievum;
using Entity.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ArchievumRepositories
{
    public interface IArchiveRepository
    {
        public Task<Archive> ArchivePost(ArchiveDTO dto);
    }
}
