using ConnectionProvider.Context;
using Domain.Entities.Archivievum;
using Domain.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ArchievumRepositories
{
    public class ArchiveRepository : IArchiveRepository
    {
        private readonly AppDbСontext _context;

        public ArchiveRepository(AppDbСontext context)
        {
            _context = context;
        }

        public async Task<Archive> ArchivePost(ArchiveDTO dto)
        {
            var archive = new Archive
            {
                Date = DateTime.Now,
                ContractName = dto.ContractName,
                ExecutorsEmail = dto.ExecutorsEmail,
                ExecutorsFullName = dto.ExecutorsFullName,
                DocumentType = dto.DocumentType,
                FilePath = dto.FilePath
            };
            await _context.Archives.AddAsync(archive);
            await _context.SaveChangesAsync();
            return archive;
        }
    } 
}
