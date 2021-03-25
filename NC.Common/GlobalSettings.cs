using Microsoft.Extensions.Configuration;

namespace NC.Common
{
    public static class GlobalSettings
    {
        public static IConfiguration Configuration { get; set; }

        public static string GetIssuer()
        {
            return Configuration.GetSection("Jwt").GetValue<string>("Issuer");
        }

        public static string GetAudience()
        {
            return Configuration.GetSection("Jwt").GetValue<string>("Audience");
        }

        public static string GetSecret()
        {
            return Configuration.GetSection("Jwt").GetValue<string>("SecretKey");
        }

        public static string GetGoogleCredentialFile()
        {
            return Configuration.GetSection("GoogleCloudStorage").GetValue<string>("CredentialFile");
        }

        public static string GetGoogleCloudStorageBucket()
        {
            return Configuration.GetSection("GoogleCloudStorage").GetValue<string>("Bucket");
        }

        public static SmtpSettings GetSmtpSettings()
        {
            return Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();
        }
    }
}
