using NC.Common.Enums;

namespace NC.Business.Models.User
{
    public class LoginResult
    {
        public LoginStatus Status { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public LoginResult(LoginStatus status)
        {
            Status = status;
        }

        public LoginResult(LoginStatus status, string message)
            : this(status)
        {
            Message = message;
        }
    }
}
