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
        Task UpdateBalance();
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
               
                UserName = "sumon@gmail.com", 
                Email = "sumon@gmail.com"
            };
            var result = await _userManager.CreateAsync(user, "tapos1234$..T");

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByNameAsync("agent");

                if (role == null)
                {
                    await _roleManager.CreateAsync(new AppRole()
                    {
                        Name = "agent"
                    });
                }
            }

            await   _userManager.AddToRoleAsync(user, "agent");

        }

        public async Task UpdateBalance()
        {
            
            Random rnd = new Random();
            int myNumber  = rnd.Next(1, 100);
            Order myOrder = new Order()
            {
                Amount = myNumber
            };

           await _unitOfWork.OrderRepository.CreateAsync(myOrder);

           if (await _unitOfWork.ApplicationSaveChangesAsync())
           {
               await _unitOfWork.CustomerBalanceRepository.UpdateCustomerBalanceAsync(myNumber);
           }
           
        }
    }
}