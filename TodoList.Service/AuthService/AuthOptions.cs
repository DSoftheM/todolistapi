using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TodoList.Service.AuthService;

public class AuthOptions
{
    public const string ISSUER = "ISSUER";
    public const string AUDIENCE = "AUDIENCE";
    public const int LIFETIME = 60;

    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
    }

    private const string KEY = "mysupersecret_mysupersecret_secretkey!123";
}