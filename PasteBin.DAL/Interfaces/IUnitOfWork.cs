using PasteBinApi.DAL.Interface;

namespace PasteBin.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPastRepositories Repository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
