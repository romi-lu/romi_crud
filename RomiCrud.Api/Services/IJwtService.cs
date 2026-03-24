using System.Security.Claims;

namespace RomiCrud.Api.Services;

public interface IJwtService
{
    string CreateToken(IEnumerable<Claim> claims, DateTime expiresAtUtc);
}
