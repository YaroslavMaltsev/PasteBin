using AutoMapper;
using PasteBin.Model;
using PasteBinApi.DTOs;
using PasteBinApi.Interface;

namespace PasteBinApi.Helpers.AutoMapper.Paste.CreateMethods
{
    public class HashUriResolver : IValueResolver<CreatePasteDto, Past, string>
    {
        private readonly IHashService _hashService;

        public HashUriResolver(IHashService hashService)
        {
            _hashService = hashService;
        }
        public string Resolve(CreatePasteDto source, Past destination, string destMember, ResolutionContext context)
        {
            var hashResult = _hashService.ToHash();
            return hashResult;
        }
    }
}