using Microsoft.AspNetCore.Identity;

namespace Learning.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
