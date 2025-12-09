namespace TourSite.APIs.Errors
{
    public class APIErrerResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public APIErrerResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }
       
        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            var message =statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "An error happened on the server",
                _ => null
            };
            return message ?? "An unexpected error occurred";
        }
    }
}
