namespace NC.Business.Models.Account
{
    public class RefreshTokenModel
    {
        public string UserId { get; set; }

        public string RefreshToken { get; set; }

        public string UserAgent { get; set; }
    }
}
