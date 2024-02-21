
using PasteBin.Services.CustomExptions;
using System.Net;
using System.Text.Json;

namespace PasteBinApi.Middleware
{
    public class GlobalExceptionsHandling : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                await HandlerExcpetionAsync(context,ex);
            }
        }

        private static Task HandlerExcpetionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = default;

            var stackTrace = string.Empty;

            string message = "";

            var exceptionType = ex.GetType();

            if(exceptionType  == typeof(ArgumentNotFoundExption)) 
            { 
                statusCode = HttpStatusCode.NotFound;
                message = ex.Message;
            }
            else if(exceptionType == typeof(ArgumentBadRequestExption))
            {
                statusCode = HttpStatusCode.BadRequest;
                message = ex.Message;
            }
            else if(exceptionType == typeof(StorageServiceException))
            {
                statusCode = HttpStatusCode.InternalServerError;
                message = ex.Message;
            }
            else if(exceptionType == typeof(Exception))
            {
                statusCode = HttpStatusCode.InternalServerError;
                message = "Unknown server error";
            }

            var exceptionResult = JsonSerializer.Serialize(new {errorMesage = message});
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
