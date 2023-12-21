using PasteBin.Data;
using PasteBinApi.Interface;

namespace PasteBinApi.Repositories
{
    public class FileRepositories : IFileRepositories
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IManageFile _manageFile;

        public FileRepositories(ApplicationDbContext dbContext, IManageFile manageFile)
        {
            _dbContext = dbContext;
            _manageFile = manageFile;
        }
        public bool Delete(IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        public Task<(byte[], string, string)> DownloadAsync(string fileName)
        {
         return _manageFile.DownloadFileAsync(fileName);
        }

        public async Task<bool> UpdateAsync(IFormFile formFile, string fileName)
        {
            {
                var updateResolve = await _manageFile.UpdateFileAsync(formFile, fileName);
                return updateResolve != 0 ? true : false;
            }
        }

    }
}
