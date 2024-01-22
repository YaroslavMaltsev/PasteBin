
using PasteBin.Domain.Interfaces;
using PasteBin.Domain.Response;
using PasteBin.Services.Interfaces;

namespace PasteBin.Services.Builder
{
    public static class BaseResponseBuilder<T>
    {
        public static IBaseResponse<T> GetBaseResponse()
        {
            return new BaseResponse<T>();
        }

        public static IBaseResponse<IEnumerable<T>> GetBaseResponseAll()
        {
            return new BaseResponse<IEnumerable<T>>();
        }
    }
}
