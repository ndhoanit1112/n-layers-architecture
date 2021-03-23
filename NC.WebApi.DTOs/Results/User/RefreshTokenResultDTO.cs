namespace NC.WebApi.DTOs.Results.User
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
