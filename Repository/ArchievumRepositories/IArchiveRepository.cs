namespace Repository.ArchievumRepositories
{
    public interface IArchiveRepository
    {
        public Task<Archive> ArchivePost(ArchiveDTO dto);
    }
}
