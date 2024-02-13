using AutoMapper;
using PasteBin.Domain.Interfaces;
using PasteBin.Domain.Model;
using PasteBin.Services.Builder;
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

        public PastService(IPastRepositories pastRepositories,
            ITimeCalculationService timeCalculation,
            IHashService hashService,
            IMapper mapper,
            IStorageS3Service storageS3Service)
        {
            _pastRepositories = pastRepositories;
            _timeCalculation = timeCalculation;
            _hashService = hashService;
            _mapper = mapper;
            _storageS3Service = storageS3Service;
        }
        public async Task<IBaseResponse<bool>> CreatePosteService(CreatePasteDto pastCreate, string userId)
        {

            var response = BaseResponseBuilder<bool>.GetBaseResponse();
            var key = Guid.NewGuid().ToString();
            try
            {
                if (pastCreate == null)
                {
                    response.Description = "BeadRequest";
                    response.StatusCode = 400;
                    return response;
                }

                var responseStorageS3Service = await _storageS3Service.UploadTextToStorage(pastCreate.Text, key);

                if (!responseStorageS3Service)
                {
                    response.Description = "S3 Service Error";
                    response.StatusCode = 500;
                    return response;
                }

                var past = new Past()
                {
                    Title = pastCreate.Title,
                    DateCreate = DateTime.Now,
                    DateDelete = _timeCalculation.GetTimeToDelete(pastCreate.DateSave),
                    HashUrl = _hashService.ToHash(),
                    Key = key,
                    UserId = userId
                };

                var responseToSave = await _pastRepositories.CreatePost(past);

                if (responseToSave == false)
                {
                    response.StatusCode = 500;
                    response.Description = "Post not Create";
                    response.Data = false;
                    return response;
                }

                response.StatusCode = 201;
                response.Description = "Post Create";
                response.Data = responseToSave;

                return response;
            }
            catch (Exception ex)
            {
                response.Description = "Server error";
                response.StatusCode = 500;
                return response;
            }
        }

        public async Task<IBaseResponse<bool>> DeletePostService(int id, string userId)
        {
            var response = BaseResponseBuilder<bool>.GetBaseResponse();

            try
            {
                if (id <= 0)
                {
                    response.Description = "BeadRequest";
                    response.StatusCode = 400;
                    return response;
                }
                var past = await _pastRepositories.GetPastById(id, userId);

                if (past == null)
                {
                    response.Description = "NotFound";
                    response.StatusCode = 404;
                    return response;
                }
                var responseDeleteTextToS3 = await _storageS3Service.DeleteTextPasteToS3(past.Key);
    
                var responseToDelete = _pastRepositories.Delete(past);

                if (!responseToDelete && !responseDeleteTextToS3)
                {
                    response.StatusCode = 500;
                    response.Description = "Post don't Delete";
                    response.Data = responseToDelete;
                }
                response.StatusCode = 200;
                response.Description = "Post on Delete";
                response.Data = responseToDelete;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Description = "Server error";
                return response;
            }
        }

        public async Task<IBaseResponse<IEnumerable<GetPastDto>>> GetPostAllService(string userId)
        {
            var response = BaseResponseBuilder<GetPastDto>.GetBaseResponseAll();

            try
            {
                var pastDto = _mapper.Map<IEnumerable<GetPastDto>>(await _pastRepositories.GetPastAll(userId));

                if (pastDto == null)
                {
                    response.StatusCode = 404;
                    response.Description = "NotFound";
                    return response;
                }

                response.StatusCode = 200;
                response.Description = "Posts";
                response.Data = pastDto;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Description = "Server error";
                return response;
            }
        }

        public async Task<IBaseResponse<GetPastDto>> GetPostByHashService(string hash)
        {
            var response = BaseResponseBuilder<GetPastDto>.GetBaseResponse();
            try
            {
                if (hash == null)
                {
                    response.StatusCode = 400;
                    response.Description = "BeadRequest";
                    return response;
                }

                var past = await _pastRepositories.GetPostByHash(hash);


                if (past == null)
                {
                    response.StatusCode = 404;
                    response.Description = "NotFound";
                    return response;
                }
                past.Views += 1;

                var pastResponse = new GetPastDto
                {
                    Id = past.Id,
                    Title = past.Title,
                    DateDelete = past.DateDelete,
                    DateCreate = past.DateCreate,
                    Views = past.Views,
                    HashUrl = past.HashUrl,
                    Text = await _storageS3Service.GetTextPasteToS3(past.Key)
                };

                var responseUpdateViews = _pastRepositories.UpdatePast(past);

                if (!responseUpdateViews)
                {
                    response.StatusCode = 500;
                    response.Description = "Server error";
                    return response;
                }
                response.StatusCode = 200;
                response.Description = "Post found";
                response.Data = pastResponse;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Description = "Server error";
                return response;
            }
        }

        public async Task<IBaseResponse<GetPastDto>> GetPostByIdService(int id, string userId)
        {
            var response = BaseResponseBuilder<GetPastDto>.GetBaseResponse();
            try
            {
                if (id <= 0)
                {
                    response.StatusCode = 400;
                    response.Description = "BeadRequest";
                    return response;
                }

                var past = await _pastRepositories.GetPastById(id, userId);

                if (past == null)
                {
                    response.StatusCode = 404;
                    response.Description = "NotFound";
                    return response;
                }

                var postResponse = new GetPastDto
                {
                    Id = past.Id,
                    Title = past.Title,
                    DateDelete = past.DateDelete,
                    DateCreate = past.DateCreate,
                    HashUrl = past.HashUrl,
                    Text = await _storageS3Service.GetTextPasteToS3(past.Key)
                };

                response.StatusCode = 200;
                response.Description = "Post found";
                response.Data = postResponse;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Description = "Server error";
                return response;
            }
        }

        public async Task<IBaseResponse<bool>> UpdatePostService(UpdatePasteDto updatePast, int id, string userId)
        {
            var response = BaseResponseBuilder<bool>.GetBaseResponse();
            try
            {
                if (updatePast == null && id <= 0)
                {
                    response.StatusCode = 400;
                    response.Description = "BeadRequest";
                    return response;
                }

                var past = await _pastRepositories.GetPastById(id, userId);

                if (past == null)
                {
                    response.StatusCode = 400;
                    response.Description = "BeadRequest";
                    return response;
                }

                past.Title = updatePast.Title;
                past.DateDelete = _timeCalculation.GetTimeToDelete(updatePast.DateSave);
                past.HashUrl = _hashService.ToString();

                var updateResolver = _pastRepositories.UpdatePast(past);

                if (updateResolver == false)
                {
                    response.StatusCode = 500;
                    response.Description = "Post not updated";
                    response.Data = updateResolver;
                }
                response.StatusCode = 200;
                response.Description = "Post updated";
                response.Data = updateResolver;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Description = "Service error";
                return response;
            }
        }
    }
}
