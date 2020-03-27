using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.DbContext;
using DLL.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository
{
    public interface IDepartmentRepository
    {
        Task<Department> AddDepartmentAsync(Department aDepartment);
        Task<List<Department>> GetAllDepartmentAsync();
        Task<Department> FindADepartmentAsync(string code);


        Task<bool> IsNameExists(string name);
        Task<bool> IsCodeExits(string code);
    }
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Department> AddDepartmentAsync(Department aDepartment)
        {
           await  _context.Departments.AddAsync(aDepartment);
           await  _context.SaveChangesAsync();
            return aDepartment;
        }

        public async Task<Department> FindADepartmentAsync(string code)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            return department;
        }

        public async Task<bool> IsNameExists(string name)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Name == name);
            if (department == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsCodeExits(string code)
        {
            var department = await _context.Departments.FirstOrDefaultAsync(x => x.Code == code);
            if (department == null)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Department>> GetAllDepartmentAsync()
        {
            return await _context.Departments.ToListAsync();
        }
    }
}