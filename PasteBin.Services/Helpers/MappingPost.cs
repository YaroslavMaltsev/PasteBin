
using AutoMapper;
using PasteBin.Domain.Model;
using PasteBinApi.Domain.DTOs;

namespace PasteBin.Services.Helpers
{
    internal class MappingPost : Profile
    {
        public MappingPost() 
        {
            CreateMap<Past, GetPastDto>();
        }
    }
}
