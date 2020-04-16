using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Response;
using DLL.Model;
using DLL.UnitOfWork;
using DLL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Utility.Exceptions;
using Utility.Helpers;

namespace BLL.Services
{
    public interface IAccountService
    {
        Task<LoginResponse> LoginUser(LoginRequest request);
        Task Test(ClaimsPrincipal tt);
        Task<SuccessResponse> Logout(ClaimsPrincipal tt);
        Task<LoginResponse> RefreshToken(string refreshToken);
    }


    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TaposRSA _taposRsa;
        private readonly IDistributedCache _cache;

        public AccountService(UserManager<AppUser> userManager,IConfiguration configuration,IUnitOfWork unitOfWork,TaposRSA taposRsa,IDistributedCache cache)
        {
            _userManager = userManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _taposRsa = taposRsa;
            _cache = cache;
        }

        public async Task<LoginResponse> LoginUser(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                throw new MyAppException("user not found");
            }

            var matchUser = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!matchUser)
            {
                throw new MyAppException("user name and password does not match");
            }

            return await GenerateJSONWebToken(user);
        }

        public async Task Test(ClaimsPrincipal tt)
        {
           
            var userId = tt.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ;
            var userName = tt.Claims.FirstOrDefault(c => c.Type == "username")?.Value ;
            var role = tt.FindFirst(ClaimTypes.Role)?.Value ;
            var email = tt.Claims.FirstOrDefault(c => c.Type == "email")?.Value ;
            var mm = tt.Claims.FirstOrDefault(c => c.Type == "geegeg")?.Value ;
            
            var department = new Department()
            {
                Code = "asdfdsf",
                Name = "safsdfsdf"
            };
            await _unitOfWork.DepartmentRepository.CreateAsync(department);

            await _unitOfWork.ApplicationSaveChangesAsync();
            
            throw new NotImplementedException();
        }

        public async Task<SuccessResponse> Logout(ClaimsPrincipal tt)
        {
            var userId = tt.FindFirst(c=>c.Type=="userid")?.Value ;
            
            var accessTokenKey = userId+ "_acesstoken";
            var refreshTokenKey = userId + "_refreshtoken";

            await _cache.RemoveAsync(accessTokenKey);
            await _cache.RemoveAsync(refreshTokenKey);
            
            return new SuccessResponse()
            {
                Message = "lgoout sucessfully",
                StatusCode = 200
            };
        }

        public async Task<LoginResponse> RefreshToken(string refreshToken)
        {
            var decryptRsa = _taposRsa.Decrypt(refreshToken, "v1");

            if (decryptRsa == null)
            {
                throw new MyAppException("refresh token not found");
            }

            var refreshTokenObject = JsonConvert.DeserializeObject<RefreshTokenResponse>(decryptRsa);
            var refreshTokenKey = refreshTokenObject.UserId + "_refreshtoken";

            var cacheData = await _cache.GetStringAsync(refreshTokenKey);

            if (cacheData == null)
            {
                throw new MyAppException("refresh token not found");
            }

            if (cacheData != refreshToken)
            {
                throw new MyAppException("refresh token not found");
            }

            var user = await _userManager.FindByIdAsync(refreshTokenObject.UserId.ToString());

            return await GenerateJSONWebToken(user);


        }


        private async Task<LoginResponse> GenerateJSONWebToken(AppUser userInfo)
        {

            var response = new LoginResponse();
            var userRole = (await _userManager.GetRolesAsync(userInfo)).FirstOrDefault();
            //username
            //mobile number
            // role 
            // user id
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Id.ToString()),
                new Claim(CustomJwtClaimsName.UserName, userInfo.UserName.ToString()),
                new Claim(CustomJwtClaimsName.Email, userInfo.Email.ToString()),
                new Claim(CustomJwtClaimsName.UserId, userInfo.Id.ToString()),
                new Claim(ClaimTypes.Role, userRole)
            };

            var times = _configuration.GetValue<int>("Jwt:AccessTokenLifeTime");
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],  
                _configuration["Jwt:Issuer"],  
                claims:claims,  
                expires: DateTime.Now.AddMinutes(times),  
                signingCredentials: credentials);  
            
            var refreshToken = new RefreshTokenResponse()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userInfo.Id
            };
            var resData = _taposRsa.EncryptData(JsonConvert.SerializeObject(refreshToken),"v1");
            response.Token  =  new JwtSecurityTokenHandler().WriteToken(token);
            
            
            response.Expire = times * 60;
            response.RefreshToken = resData;

            await StoreTokenInformation(userInfo.Id, response.Token, response.RefreshToken);
            return response;
        }

        private async Task StoreTokenInformation(long userId, string accessToken, string refreshToken)
        {
           
            var accessTokenOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(_configuration.GetValue<int>("Jwt:AccessTokenLifeTime")));
            
            var refreshTokenOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(_configuration.GetValue<int>("Jwt:RefreshTokenLifeTime")));

            var accessTokenKey = userId.ToString() + "_acesstoken";
            var refreshTokenKey = userId.ToString() + "_refreshtoken";

            await _cache.SetStringAsync(accessTokenKey, accessToken, accessTokenOptions);
            await _cache.SetStringAsync(refreshTokenKey, refreshToken, refreshTokenOptions);
        }
    }

    
}