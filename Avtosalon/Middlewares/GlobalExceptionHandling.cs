using Avtosalon.Models.Exceptions;

namespace Avtosalon.Middlewares
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate next;
        public GlobalExceptionHandling(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch(Exception exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = exception switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    ValidationException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };

                var response = new
                {
                    Status = context.Response.StatusCode,
                    Title = "Xatolik yuz berdi",
                    Detail = exception switch
                    {
                        NotFoundException => exception.Message,
                        ValidationException => exception.Message,
                        _ => "Serverda qandaydir xatolik yuz berdi."
                    }
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
