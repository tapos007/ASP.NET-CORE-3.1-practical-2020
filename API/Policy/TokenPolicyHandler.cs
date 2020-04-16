using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Policy
{
    public class TokenPolicyHandler : AuthorizationHandler<TokenPolicy>
    {
        private readonly IDistributedCache _cache;

        public TokenPolicyHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenPolicy requirement)
        {
            
            
            var userId = context.User.Claims.FirstOrDefault(x => x.Type == "userid")?.Value;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            
          
            var accessTokenKey = userId + "_acesstoken";
            var cacheToken = _cache.GetString(accessTokenKey);

            if (cacheToken == null)
            {
                throw new UnauthorizedAccessException();
            }
            else
            {
                context.Succeed(requirement);
            }
            
            return  Task.CompletedTask;

        }
    }
}