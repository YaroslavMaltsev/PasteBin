using AutoMapper;
using PasteBin.Model;
using PasteBinApi.Dto;
using PasteBinApi.Helpers.AutoMapper.Paste.CreateMethods;
using PasteBinApi.Helpers.AutoMapper.Paste.UpdateMethods;
using PasteBinApi.ResourceModel;

namespace PasteBinApi.Helpers.AutoMapper.Paste
{
    public class PasteProfile : Profile
    {

        public PasteProfile()
        {
            CreateMap<UpdatePastDto, Past>()
                .ForMember
                (
                dest => dest.Title,
                opt => opt.MapFrom(src => src.Title))
                .ForMember
                (
                dest => dest.DateDelete,
                opt => opt.MapFrom<TimeCalculationResolver>()
                );

            CreateMap<Past,GetPastDto>();

            CreateMap<CreatePastDto, Past>()
                .ForMember
                (
                dest => dest.Title,
                opt => opt.MapFrom(src => src.Title)
                )
                .ForMember
                (
                 dest => dest.DateDelete,
                 opt => opt.MapFrom<TimeCalculatorResolver>()
                )
                .ForMember
                (
                dest => dest.DtateCreate,
                opt => opt.MapFrom(src => DateTime.Now)
                )
                .ForMember
                (
                dest => dest.URL,
                opt => opt.MapFrom<FileResolver>()
                )
                .ForMember
                (
                dest => dest.HashUrl,
                opt => opt.MapFrom<HashUriResolver>()
                );
        }
    }
}
