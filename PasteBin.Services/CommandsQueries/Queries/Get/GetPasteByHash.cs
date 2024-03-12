using AutoMapper;
using PasteBin.Services.CustomExptions;
using PasteBin.Services.Interfaces;
using PasteBinApi.DAL.Interface;
using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.CommandsQueries.Queries.Get
{
    public class GetPasteByHash : IGetPasteByHash
    {
        private readonly IPastRepositories _pastRepositories;
        private readonly IMapper _mapper;
        private readonly IStorageS3Service _storageS3Service;

        public GetPasteByHash(IPastRepositories pastRepositories,
            IMapper mapper,
            IStorageS3Service storageS3Service)
        {
            _pastRepositories = pastRepositories;
            _mapper = mapper;
            _storageS3Service = storageS3Service;
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
            catch (StorageServiceException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
