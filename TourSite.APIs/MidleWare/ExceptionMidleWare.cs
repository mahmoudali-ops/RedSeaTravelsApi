using System.Text.Json;
using TourSite.APIs.Errors;

namespace TourSite.APIs.MidleWare
{
    public class ExceptionMidleWare
    {
        public RequestDelegate Next { get; }
        public ILogger<ExceptionMidleWare> Logger { get; }
        public IHostEnvironment Env { get; }
        public ExceptionMidleWare(RequestDelegate next,ILogger<ExceptionMidleWare> logger,IHostEnvironment env)
        {
            Next = next;
            Logger = logger;
            Env = env;
        }

        public async Task InvokeAsync(HttpContext Context) 
        {
            try
            {
                await Next.Invoke(Context);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,ex.Message);

                Context.Response.ContentType = "application/json";
                Context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = Env.IsDevelopment() ?
                     new ExceptionErrorResponse(StatusCodes.Status500InternalServerError, ex.Message, ex?.StackTrace?.ToString()):
                                          new APIErrerResponse(StatusCodes.Status500InternalServerError) ;

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response,options);
                await Context.Response.WriteAsync(json);
            }
        }

       
    }
}
