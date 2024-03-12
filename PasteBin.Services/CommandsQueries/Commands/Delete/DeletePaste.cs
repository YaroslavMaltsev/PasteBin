using PasteBin.DAL.Interfaces;
using PasteBin.Services.CustomExptions;
using PasteBin.Services.Interfaces;
using PasteBinApi.DAL.Interface;

namespace PasteBin.Services.CommandsQueries.Commands.Delete
{
    public class DeletePaste : IDeletePaste
    {
        private readonly IPastRepositories _pastRepositories;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStorageS3Service _storageS3Service;

        public DeletePaste(IPastRepositories pastRepositories,
            IUnitOfWork unitOfWork,
            IStorageS3Service storageS3Service)
        {
            _pastRepositories = pastRepositories;
            _unitOfWork = unitOfWork;
            _storageS3Service = storageS3Service;
        }

        public async Task DeletePostServiceAsync(int id, string userId)
        {
            try
            {
                if (id <= 0 && userId == null)
                {
                    throw new ArgumentBadRequestExption("Check your details and try again later");
                }

                var past = await _pastRepositories.GetPastByIdAsync(id, userId);

                if (past == null)
                {
                    throw new ArgumentNotFoundExption($"Paste with this id was not found : id {id}");
                }

                using (var unitOfWork = _unitOfWork)
                {
                    try
                    {
                        await unitOfWork.BeginTransactionAsync();

                        _pastRepositories.Delete(past);
                        await _unitOfWork.SaveChangesAsync();

                        var responseDeleteTextToS3 = await _storageS3Service.DeleteTextPasteToS3Async(past.Key);

                        if (!responseDeleteTextToS3)
                        {
                            throw new StorageServiceException("S3 Service Erorr");
                        }

                        await _unitOfWork.CommitTransactionAsync();
                    }
                    catch (StorageServiceException ex)
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        throw;
                    }
                    catch (Exception)
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
