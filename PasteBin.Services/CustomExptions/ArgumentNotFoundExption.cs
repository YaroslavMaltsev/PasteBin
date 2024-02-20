namespace PasteBin.Services.CustomExptions
{
    public class ArgumentNotFoundExption : Exception
    {
        public ArgumentNotFoundExption(string? message) : base(message)
        {
        }
    }
}
