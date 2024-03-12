
using PasteBin.DAL.Interfaces;
using PasteBin.Domain.DTOs;
using PasteBin.Domain.Model;
using PasteBin.Services.CustomExptions;
using PasteBin.Services.Interfaces;
using PasteBinApi.DAL.Interface;
using PasteBinApi.Domain.DTOs;
using PasteBinApi.Services.Interface;

namespace PasteBin.Services.CommandsQueries.Commands.Create
{
    public class CreatePaste : ICreatePaste
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeCalculationService _timeCalculation;
        private readonly IHashService _hashService;
        private readonly IPastRepositories _pastRepositories;
        private readonly IStorageS3Service _storageS3Service;

        public CreatePaste(IUnitOfWork unitOfWork,
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

        public async Task<ResponseCreateDto> CreatePosteServiceAsync(CreatePasteDto pastCreate, string userId)
        {

            try
            {
                if (pastCreate == null && userId == null)
                {
                    throw new ArgumentBadRequestExption("Check your details and try again later");
                }

                var key = Guid.NewGuid().ToString();

                var past = new Past()
                {
                    Title = pastCreate.Title,
                    DateCreate = DateTime.Now,
                    DateDelete = _timeCalculation.GetTimeToDelete(pastCreate.DateSave),
                    HashUrl = _hashService.ToHash(),
                    Key = key,
                    UserId = userId
                };

                using (var unitOfWork = _unitOfWork)
                {

                    try
                    {
                        await unitOfWork.BeginTransactionAsync();

                        await _pastRepositories.CreatePostAsync(past);
                        await _unitOfWork.SaveChangesAsync();

                        var responseStorageS3Service = await _storageS3Service.UploadTextToStorageAsync(pastCreate.Text, key);

                        if (!responseStorageS3Service)
                        {
                            throw new StorageServiceException("S3 Service Erorr");
                        }

                        await _unitOfWork.CommitTransactionAsync();

                        return new ResponseCreateDto
                        {
                            id = past.Id,
                            hash = past.HashUrl

                        };

                    }
                    catch (StorageServiceException)
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
