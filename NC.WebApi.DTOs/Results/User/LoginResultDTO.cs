namespace NC.WebApi.DTOs.Results.User
{
    public class LoginResultDTO
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
