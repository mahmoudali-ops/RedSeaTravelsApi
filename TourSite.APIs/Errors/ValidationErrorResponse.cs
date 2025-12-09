namespace TourSite.APIs.Errors
{
    public class ValidationErrorResponse : APIErrerResponse
    {
        public IEnumerable<string> errors { get; set; } = new List<string>();

        public ValidationErrorResponse() : base(400)
        {
            
        }
    }
}
