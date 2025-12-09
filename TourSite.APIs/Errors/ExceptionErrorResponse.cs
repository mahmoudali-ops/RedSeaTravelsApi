namespace TourSite.APIs.Errors
{
    public class ExceptionErrorResponse :APIErrerResponse
    {

        public string? Details { get; set; }
        public ExceptionErrorResponse(int statusCode,string? errorMessage,string? Detail=null)
            :base(statusCode, errorMessage)
        {
            Details=Detail;
        }

    }
}
