using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TeamTaskTracker.UI.Services;
public static class JwtParser
{
    public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);

        return token.Claims;
    }
}
