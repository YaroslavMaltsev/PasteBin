using AutoMapper;
using PasteBin.Model;
using PasteBinApi.DTOs;
using PasteBinApi.Helpers.AutoMapper.Paste.CreateMethods;

namespace PasteBinApi.Helpers.AutoMapper.Paste
{
    public class PasteProfile : Profile
    {

        public PasteProfile()
        {
            //CreateMap<UpdateFileDto, Past>()
            //    .ForMember
            //    (
            //    dest => dest.Id,
            //    opt => opt.MapFrom(scr => scr.Id)
            //    )
            //    .ForMember
            //    (
            //    dest => dest.URL,
            //    opt => opt.MapFrom<UpdateFileResolver>()
            //    ).ForMember
            //    (
            //    dest => dest.HashUrl,
            //    opt => opt.MapFrom(scr => scr) 
            //    );

            //CreateMap<UpdatePasteDto, Past>()
            //    .ForMember
            //    (
            //    dest => dest.Title,
            //    opt => opt.MapFrom(src => src.Title))
            //    .ForMember
            //    (
            //    dest => dest.DateDelete,
            //    opt => opt.MapFrom<UpdateTimeCalculationResolver>()
            //    )
            //    .ForMember
            //    (
            //    dest => dest.HashUrl,
            //    opt => opt.MapFrom<UpdateHashUriResolver>()
            //    );


            CreateMap<Past, GetPastDto>();

            CreateMap<CreatePasteDto, Past>()
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
                opt => opt.MapFrom<UploadFileResolver>()
                )
                .ForMember
                (
                dest => dest.HashUrl,
                opt => opt.MapFrom<HashUriResolver>()
                );
        }
    }
}
