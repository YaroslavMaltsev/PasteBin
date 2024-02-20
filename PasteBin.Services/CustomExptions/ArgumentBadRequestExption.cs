
namespace PasteBin.Services.CustomExptions
{
    public class ArgumentBadRequestExption : Exception
    {
        public ArgumentBadRequestExption(string? message) : base(message)
        {
        }
    }
}
