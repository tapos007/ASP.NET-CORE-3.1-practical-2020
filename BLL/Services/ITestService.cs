using System;
using System.Threading.Tasks;
using DLL.Model;
using DLL.Repository;
using DLL.UnitOfWork;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public interface ITestService
    {
        Task SaveAllData();
    }

    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;


        public TestService(IUnitOfWork unitOfWork,UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SaveAllData()
        {
            
            var user = new AppUser() {
               
                UserName = "tapos.aa@gmail.com", 
                Email = "tapos.aa@gmail.com"
            };
            var result = await _userManager.CreateAsync(user, "tapos1234$..T");

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("customer");

                if (role == null)
                {
                    await _roleManager.CreateAsync(new AppRole()
                    {
                        Name = "customer"
                    });
                }
            }

            await   _userManager.AddToRoleAsync(user, "customer");

        }
        
    }
}