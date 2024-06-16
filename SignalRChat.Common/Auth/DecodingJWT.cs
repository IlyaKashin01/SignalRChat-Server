using System.IdentityModel.Tokens.Jwt;

namespace SignalRChat.Common.Auth
{
    public class DecodingJWT: IDecodingJWT
    {
        
        public string? getJWTTokenClaim(string token, string claimName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            var claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;
            return claimValue;
        }
    }
}
