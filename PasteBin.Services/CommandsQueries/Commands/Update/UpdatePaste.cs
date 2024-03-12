using PasteBin.DAL.Interfaces;
using PasteBin.Services.CustomExptions;
using PasteBin.Services.Interfaces;
using PasteBinApi.DAL.Interface;
using PasteBinApi.Domain.DTOs;
using PasteBinApi.Services.Interface;

namespace PasteBin.Services.CommandsQueries.Commands.Update
{
    public class UpdatePaste : IUpdatePaste
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeCalculationService _timeCalculation;
        private readonly IHashService _hashService;
        private readonly IPastRepositories _pastRepositories;
        private readonly IStorageS3Service _storageS3Service;

        public UpdatePaste(IUnitOfWork unitOfWork,
            ITimeCalculationService timeCalculation,
            IHashService hashService,
            IPastRepositories pastRepositories,
            IStorageS3Service storageS3Service)
        {
            _unitOfWork = unitOfWork;
            _timeCalculation = timeCalculation;
            _hashService = hashService;
            _pastRepositories = pastRepositories;
            _storageS3Service = storageS3Service;
        }

        public async Task UpdatePostServiceAsync(UpdatePasteDto updatePast, int id, string userId)
        {
            try
            {
                if (updatePast == null && id <= 0)
                {
                    throw new ArgumentBadRequestExption("Check your details and try again later");
                }

                var past = await _pastRepositories.GetPastByIdAsync(id, userId);

                if (past == null)
                {
                    throw new ArgumentNotFoundExption($"Paste with this id was not found : id {id}");
                }

                past.Title = updatePast.Title;
                past.DateDelete = _timeCalculation.GetTimeToDelete(updatePast.DateSave);
                past.HashUrl = _hashService.ToString();

                using (var unitOfWork = _unitOfWork)
                {
                    try
                    {
                        await unitOfWork.BeginTransactionAsync();

                        _pastRepositories.UpdatePast(past);
                        await _unitOfWork.SaveChangesAsync();

                        var responseTextToS3 = await _storageS3Service.UploadTextToStorageAsync(updatePast.Text, past.Key);

                        if (!responseTextToS3)
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
                    catch (Exception ex)
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

