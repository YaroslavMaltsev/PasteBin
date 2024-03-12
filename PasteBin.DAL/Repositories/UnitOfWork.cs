


using PasteBin.DAL.Data;
using PasteBin.DAL.Interfaces;
using PasteBinApi.DAL.Interface;

namespace PasteBin.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPastRepositories _pastRepositories;

        public UnitOfWork(ApplicationDbContext applicationDbContext, 
            IPastRepositories pastRepositories)
        {
            _applicationDbContext = applicationDbContext;
            _pastRepositories = pastRepositories;
        }

        public IPastRepositories Repository => _pastRepositories;  

        public async Task BeginTransactionAsync()
        {
           await _applicationDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await  _applicationDbContext.Database.CommitTransactionAsync();
            }
            catch
            {
                await _applicationDbContext.Database.RollbackTransactionAsync();
            }
            finally
            {
                await _applicationDbContext.DisposeAsync();
               
            }

        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _applicationDbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public async Task RollbackTransactionAsync()
        {
            await _applicationDbContext.Database.RollbackTransactionAsync();
            await _applicationDbContext.DisposeAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _applicationDbContext.SaveChangesAsync();
        }

    }
}
