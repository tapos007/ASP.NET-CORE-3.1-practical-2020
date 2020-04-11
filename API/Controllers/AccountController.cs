using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    public class AccountController : OurApplicationController
    {
        private readonly IAccountService _accountService;


        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login( LoginRequest request)
        {
            return Ok(await _accountService.LoginUser(request));
        }

        [HttpGet("test1")]
        public ActionResult Test1()
        {
            
            return Ok("enter test 1");
        }
        
        
        
        [HttpGet("test2")]
        [Authorize(Roles = "customer")]
        public async Task<ActionResult> Test2()
        {
            var tt = User;

           await _accountService.Test(tt); 
            return Ok("enter test 2");
        }
        
        [HttpGet("test3")]
        [Authorize(Roles = "agent")]
        public ActionResult Test3()
        {
            var tt = User;

            _accountService.Test(tt); 
            return Ok("enter test 3");
        }
        
        
    }
}