
using System.Net;

namespace Learning.API.Middlewares
{
    public class ExceptionHandleMiddleware
    {
        private readonly ILogger<ExceptionHandleMiddleware> logger;
        private readonly RequestDelegate next;
        public ExceptionHandleMiddleware(ILogger<ExceptionHandleMiddleware> logger,
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //Log this Exception
                logger.LogError(ex, $"{errorId}:{ex.Message}");

                //Return a custom Error Response
                httpContext.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something Went wrong we are looking into resolving this.",
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }

    }
}
