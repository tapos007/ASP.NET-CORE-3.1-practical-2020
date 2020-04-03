using System;
using System.Threading.Tasks;
using DLL.Model;
using DLL.Repository;
using DLL.UnitOfWork;

namespace BLL.Services
{
    public interface ITestService
    {
        Task SaveAllData();
    }

    public class TestService : ITestService
    {
        private readonly IUnitOfWork _unitOfWork;


        public TestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }

        public async Task SaveAllData()
        {
            var department = new Department()
            {
                Code = "abc",
                Name = "abc"
               
                
            };

            var aStudent = new Student()
            {
                Email = "abc@gmail.com",
                Name = "abc",
                RollNo = "006"
            };
            await _unitOfWork.DepartmentRepository.CreateAsync(department);
            await _unitOfWork.StudentRepository.CreateAsync(aStudent);
            await _unitOfWork.ApplicationSaveChangesAsync();
        }
    }
}