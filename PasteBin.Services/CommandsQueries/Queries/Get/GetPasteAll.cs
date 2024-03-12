
using AutoMapper;
using PasteBin.Services.CustomExptions;
using PasteBin.Services.Interfaces;
using PasteBinApi.DAL.Interface;
using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.CommandsQueries.Queries.Get
{
    public class GetPasteAll : IGetPasteAll
    {
        private readonly IMapper _mapper;
        private readonly IPastRepositories _pastRepositories;

        public GetPasteAll(IMapper mapper,
            IPastRepositories pastRepositories)
        {
            _mapper = mapper;
            _pastRepositories = pastRepositories;
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
    }
}
