using System.Text;

namespace PasteBinApi.Service
{
    public static class HashService
    {
        public static string ToHash()
        {
            var toByty = Encoding.UTF8.GetBytes(DateTime.Now.Microsecond.ToString());
            return Convert.ToBase64String(toByty);
        }
    }
}
