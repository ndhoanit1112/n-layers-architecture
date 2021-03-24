namespace NC.WebApi.DTOs.Results.Account
{
    public class RefreshTokenResultDTO
    {
        public string NewAccessToken { get; set; }

        public RefreshTokenResultDTO(string newAccessToken)
        {
            NewAccessToken = newAccessToken;
        }
    }
}
