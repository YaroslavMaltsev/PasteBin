using System.Text;

namespace PasteBinApi.Service
{
    public static class HashService
    {
        public static string ToHash(string tohash)
        {
            var toByty = Encoding.UTF8.GetBytes(tohash);
            return  Convert.ToBase64String(toByty);
        }
    }
}
