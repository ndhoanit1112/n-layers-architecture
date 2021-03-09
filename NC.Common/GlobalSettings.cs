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
    }
}
