using AutoMapper;
using PasteBin.Model;
using PasteBinApi.DTOs;
using PasteBinApi.Interface;

namespace PasteBinApi.Helpers.AutoMapper.Paste.CreateMethods
{
    public class UploadFileResolver : IValueResolver<CreatePasteDto, Past, string>
    {
        private readonly IManageFile _manageFile;

        public UploadFileResolver(IManageFile manageFile)
        {
            _manageFile = manageFile;
        }
        public string Resolve(CreatePasteDto source, Past destination, string destMember, ResolutionContext context)
        {
            return _manageFile.UploadFileAsync(source.formFile).Result;
        }
    }
}
