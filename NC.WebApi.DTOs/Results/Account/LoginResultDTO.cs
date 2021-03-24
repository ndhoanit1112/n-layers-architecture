namespace NC.WebApi.DTOs.Results.Account
{
    public class LoginResultDTO
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
