using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IAccountService
    {
        Task<string> LoginUser(LoginRequest request);
    }


    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<AppUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<string> LoginUser(LoginRequest request)
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

            return GenerateJSONWebToken(user);
        }
        
        
        
        private string GenerateJSONWebToken(AppUser userInfo)  
        {  
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  
  
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],  
                _configuration["Jwt:Issuer"],  
                null,  
                expires: DateTime.Now.AddSeconds(10),  
                signingCredentials: credentials);  
  
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }  
    }
}