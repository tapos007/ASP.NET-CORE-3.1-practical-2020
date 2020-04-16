using Microsoft.AspNetCore.Authorization;

namespace API.Policy
{
    public class TokenPolicy :  IAuthorizationRequirement
    {
        public TokenPolicy()
        {
        }
    }
}