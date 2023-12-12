using AutoMapper;
using PasteBin.Model;
using PasteBinApi.Interface;
using PasteBinApi.ResourceModel;

namespace PasteBinApi.Helpers.AutoMapper.Paste.CreateMethods
{
    public class FileResolver : IValueResolver<CreatePastDto, Past, string>
    {
        private readonly IManageFile _manage;

        public FileResolver(IManageFile manage)
        {
            _manage = manage;
        }
        public string Resolve(CreatePastDto source, Past destination, string destMember, ResolutionContext context)
        {
            var fileName = _manage.UploadFileAsync(source.formFile).Result;
            return fileName;
        }
    }
}
