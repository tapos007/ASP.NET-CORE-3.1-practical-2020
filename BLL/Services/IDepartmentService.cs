using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.Repository;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<Department> AddDepartmentAsync(DepartInsertRequest request);
        Task<List<Department>> GetAllDepartmentAsync();
        Task<Department> FindADepartmentAsync(string code);

        Task<Boolean> IsNameExists(string name);
        Task<Boolean> IsCodeExits(string name);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<Department> AddDepartmentAsync(DepartInsertRequest request)
        {
            Department department = new Department()
            {
                Code = request.Code,
                Name = request.Name
            };
            return await _departmentRepository.AddDepartmentAsync(department);
        }

        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            return await _departmentRepository.GetAllDepartmentAsync();
        }

        public async Task<Department> FindADepartmentAsync(string code)
        {
            return await _departmentRepository.FindADepartmentAsync(code);
        }

        public async Task<bool> IsNameExists(string name)
        {
            return await _departmentRepository.IsNameExists(name);
        }

        public async Task<bool> IsCodeExits(string code)
        {
            return await _departmentRepository.IsCodeExits(code);
        }
    }
}