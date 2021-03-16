namespace NC.WebApi.DTOs.Results
{
    public class ApiResponse
    {
        public int Code { get; set; }

        public string ErrorMessage { get; set; }

        public ApiResponse(int code, string errorMessage)
        {
            Code = code;
            ErrorMessage = errorMessage;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; set; }

        public ApiResponse(int code, string errorMessage, T result)
            : base(code, errorMessage)
        {
            Result = result;
        }
    }
}
