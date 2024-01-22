using PasteBinApi.Services.Interface;
using System.Text;

namespace PasteBinApi.Services.Service
{
    public class HashService : IHashService
    {
        public string ToHash()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes((DateTime.Now.Microsecond * DateTime.Now.Millisecond << DateTime.Now.Nanosecond).ToString()));
        }
    }
}
