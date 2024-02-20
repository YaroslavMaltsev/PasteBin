
using PasteBin.Domain.Interfaces;
using PasteBin.Domain.Response;
namespace PasteBin.Services.Builder
{
    public static class BaseResponseBuilder<T>
    {
        public static IBaseResponse<T> GetBaseResponse()
        {
            return new BaseResponse<T>();
        }
    }
}
