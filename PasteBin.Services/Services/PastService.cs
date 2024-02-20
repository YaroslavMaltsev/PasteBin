using AutoMapper;
using PasteBin.DAL.Interfaces;
using PasteBin.Domain.DTOs;
using PasteBin.Domain.Model;
using PasteBin.Services.CustomExptions;
using PasteBin.Services.Interfaces;
using PasteBinApi.DAL.Interface;
using PasteBinApi.Domain.DTOs;
using PasteBinApi.Services.Interface;

namespace PasteBin.Services.Services
{
    public class PastService : IPasteService
    {
        private readonly IPastRepositories _pastRepositories;
        private readonly ITimeCalculationService _timeCalculation;
        private readonly IHashService _hashService;
        private readonly IMapper _mapper;
        private readonly IStorageS3Service _storageS3Service;
        private readonly IUnitOfWork _unitOfWork;

        public PastService(IPastRepositories pastRepositories,
            ITimeCalculationService timeCalculation,
            IHashService hashService,
            IMapper mapper,
            IStorageS3Service storageS3Service,
            IUnitOfWork unitOfWork)
        {
            _pastRepositories = pastRepositories;

            _timeCalculation = timeCalculation;

            _hashService = hashService;

            _mapper = mapper;

            _storageS3Service = storageS3Service;

            _unitOfWork = unitOfWork;
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

        public async Task<IEnumerable<GetPastDto>> GetPostAllServiceAsync(string userId)
        {

            try
            {
                var pastDto = _mapper.Map<IEnumerable<GetPastDto>>(await _pastRepositories.GetPastAllAsync(userId));

                if (pastDto == null)
                {
                    throw new ArgumentNotFoundExption("No pastes found for this user");
                }
                return pastDto;
            }
            catch (ArgumentNotFoundExption)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetPastDto> GetPostByHashServiceAsync(string hash)
        {
            try
            {
                if (hash == null)
                {
                    throw new ArgumentBadRequestExption("Please check the details");
                }

                var past = await _pastRepositories.GetPostByHashAsync(hash);

                if (past == null)
                {
                    throw new ArgumentNotFoundExption("Paste with this hash was not found");
                }

                var pasteDto = _mapper.Map<GetPastDto>(past);

                var responseTextToS3 = await _storageS3Service.GetTextPasteToS3Async(past.Key);
                
                if (responseTextToS3 == null)
                {
                    throw new StorageServiceException("S3 Service Erorr");
                }

                pasteDto.Text = responseTextToS3;

                return pasteDto;
            }
            catch (ArgumentBadRequestExption)
            {
                throw;
            }
            catch (ArgumentNotFoundExption)
            {
                throw;
            }
            catch(StorageServiceException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetPastDto> GetPostByIdServiceAsync(int id, string userId)
        {
            try
            {
                if (id <+ 0)
                {
                    throw new ArgumentBadRequestExption("Please check the details");
                }

                var past = await _pastRepositories.GetPastByIdAsync(id, userId);


                if (past == null)
                {
                    throw new ArgumentNotFoundExption($"Paste with this id was not found : id {id}");
                }

                var pasteDto = _mapper.Map<GetPastDto>(past);

                var responseTextToS3 = await _storageS3Service.GetTextPasteToS3Async(past.Key);

                if (responseTextToS3 == null)
                {
                    throw new StorageServiceException("S3 Service Erorr");
                }

                pasteDto.Text = responseTextToS3;

                return pasteDto;
            }
            catch (ArgumentBadRequestExption)
            {
                throw;
            }
            catch (ArgumentNotFoundExption)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
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

