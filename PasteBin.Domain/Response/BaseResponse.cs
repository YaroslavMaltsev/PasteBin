
using PasteBin.Domain.Interfaces;

namespace PasteBin.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public int StatusCode { get; set; }
        public T Data { get; set; }
    }
}
