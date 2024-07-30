namespace Order_Management.Errors
{
	public class ApiValidationErrors : ApiResponse
	{
        public ApiValidationErrors():base(400)
        {
            Errors = new List<string>();
        }
        public IEnumerable<string> Errors { get; set; }
	}
}
