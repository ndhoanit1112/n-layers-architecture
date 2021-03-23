namespace NC.WebApi.DTOs.Results.User
{
    public class LoginResultDTO
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
