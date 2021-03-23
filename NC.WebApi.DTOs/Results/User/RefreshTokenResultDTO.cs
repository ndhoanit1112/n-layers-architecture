namespace NC.WebApi.DTOs.Results.User
{
    public class RefreshTokenResultDTO
    {
        public bool IsRefreshSuccess { get => !string.IsNullOrEmpty(NewAccessToken); }

        public string NewAccessToken { get; set; }

        public RefreshTokenResultDTO(string newAccessToken)
        {
            NewAccessToken = newAccessToken;
        }
    }
}
