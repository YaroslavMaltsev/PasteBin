using PasteBinApi.Interface;
using System.Text;

namespace PasteBinApi.Service
{
    public class HashService : IHashService
    {
        public string ToHash()
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(DateTime.Now.Ticks.ToString()));
        }
    }
}
