﻿using AutoMapper;
using PasteBin.Model;
using PasteBinApi.DTOs;
using PasteBinApi.Interface;

namespace PasteBinApi.Helpers.AutoMapper.Paste.CreateMethods
{
    public class FileResolver : IValueResolver<CreatePasteDto, Past, string>
    {
        private readonly IManageFile _manage;

        public FileResolver(IManageFile manage)
        {
            _manage = manage;
        }
        public string Resolve(CreatePasteDto source, Past destination, string destMember, ResolutionContext context)
        {
            var fileName = _manage.UploadFileAsync(source.formFile).Result;
            return fileName;
        }
    }
}