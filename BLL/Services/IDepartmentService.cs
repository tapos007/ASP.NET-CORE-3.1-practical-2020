using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.Repository;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IDepartmentService
    {
        Task<Department> AddDepartmentAsync(DepartInsertRequest request);
        Task<List<Department>> GetAllDepartmentAsync();
        Task<Department> FindADepartmentAsync(string code);

        Task<Boolean> IsNameExists(string name);
        Task<Boolean> IsCodeExits(string name);
        Task<Department> DeleteDepartmentAsync(string code);
        Task<Department> UpdateDepartmentAsync(string code, DepertmentUpdateRequest aDepartment);
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
            await _departmentRepository.CreateAsync(department);

            if (await _departmentRepository.ApplicationSaveChangesAsync())
            {
                return department;
            }
            throw new MyAppException("something went wrong");
        }

        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            return await _departmentRepository.GetListAsynce();
        }

        public async Task<Department> FindADepartmentAsync(string code)
        {
            var department = await _departmentRepository.GetAAsynce(x=>x.Code== code);
            if (department == null)
            {
                throw new MyAppException("department not found");
            }

            return department;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _departmentRepository.GetAAsynce(x => x.Name == name);
            if (department != null)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsCodeExits(string code)
        {
            var department = await _departmentRepository.GetAAsynce(x => x.Code == code);
            if (department != null)
            {
                return true;
            }

            return false;
        }

        public async Task<Department> DeleteDepartmentAsync(string code)
        {
            var department = await _departmentRepository.GetAAsynce(x => x.Code == code);

            if (department == null)
            {
                throw new MyAppException("department not found");
            }
            _departmentRepository.DeleteAsync(department);
            
            if (await _departmentRepository.ApplicationSaveChangesAsync())
            {
                return department;
            }
            throw new MyAppException("something went wrong");
        }

        public async Task<Department> UpdateDepartmentAsync(string code, DepertmentUpdateRequest aDepartment)
        {
            var department = await _departmentRepository.GetAAsynce(x => x.Code == code);

            if (department == null)
            {
                throw new MyAppException("department not found");
            }

            if (!string.IsNullOrWhiteSpace(aDepartment.Code))
            {
                var isCodeExistsAnotherDepartment = await _departmentRepository.GetAAsynce(x =>
                    x.Code == aDepartment.Code
                    && x.DepartmentId != department.DepartmentId);
                if (isCodeExistsAnotherDepartment == null)
                {
                    department.Code = aDepartment.Code;
                }
                else
                {
                    throw new MyAppException("code already exists different department");
                }
            }
            
            if (!string.IsNullOrWhiteSpace(aDepartment.Name))
            {
                var isCodeExistsAnotherDepartment = await _departmentRepository.GetAAsynce(x =>
                    x.Name == aDepartment.Name
                    && x.DepartmentId != department.DepartmentId);
                if (isCodeExistsAnotherDepartment == null)
                {
                    department.Name = aDepartment.Name;
                }
                else
                {
                    throw new MyAppException("name already exists different department");
                }
            }
            
            _departmentRepository.UpdateAsyc(department);
            
            if (await _departmentRepository.ApplicationSaveChangesAsync())
            {
                return department;
            }
            throw new MyAppException("something went wrong");
        }
    }
}