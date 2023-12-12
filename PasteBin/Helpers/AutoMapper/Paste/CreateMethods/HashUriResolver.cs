using AutoMapper;
using PasteBin.Model;
using PasteBinApi.Interface;
using PasteBinApi.ResourceModel;

namespace PasteBinApi.Helpers.AutoMapper.Paste.CreateMethods
{
    public class HashUriResolver : IValueResolver<CreatePastDto, Past, string>
    {
        private readonly IHashService _hashService;

        public HashUriResolver(IHashService hashService)
        {
            _hashService = hashService;
        }
        public string Resolve(CreatePastDto source, Past destination, string destMember, ResolutionContext context)
        {
            var hashResult = _hashService.ToHash();
            return hashResult;
        }
    }
}
