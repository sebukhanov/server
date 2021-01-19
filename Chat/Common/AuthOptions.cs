using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Chat.Common
{
    public class AuthOptions
    {
        public const string ISSUER = "Server";
        public const string AUDIENCE = "Client";
        const string KEY = "S03aF8!1weergtgr";
        public const int LIFETIME = 100000;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
