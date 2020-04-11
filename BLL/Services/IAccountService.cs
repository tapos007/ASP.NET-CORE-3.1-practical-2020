using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.UnitOfWork;
using DLL.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IAccountService
    {
        Task<string> LoginUser(LoginRequest request);
        Task Test(ClaimsPrincipal tt);
    }


    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(UserManager<AppUser> userManager,IConfiguration configuration,IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
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


        private async Task<string> GenerateJSONWebToken(AppUser userInfo)
        {

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
                new Claim(ClaimTypes.Role, userRole)
            };
  
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],  
                _configuration["Jwt:Issuer"],  
                claims:claims,  
                expires: DateTime.Now.AddMinutes(10),  
                signingCredentials: credentials);  
  
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }  
    }

    
}