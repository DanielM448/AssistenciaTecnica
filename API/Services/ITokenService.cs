using Models;
using System.Security.Claims;

namespace API.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserModel user);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
