﻿using NC.Common.Enums;

namespace NC.BusinessModel.User
{
    public class LoginResult
    {
        public LoginStatus Status { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }

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
