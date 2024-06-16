namespace SignalRChat.Common.Auth
{
    public interface IDecodingJWT
    {
        string? getJWTTokenClaim(string token, string claimName);
    }
}
