using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.DbContext;
using DLL.Model;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository
{
    public interface IStudentRepository
    {
        Task<Student> AddStudentAsync(Student aStudent);
        Task<Student> UpdateStudentAsync(Student aStudent);
        Task<Student> DeleteStudentAsync(string rollNo);
        Task<List<Student>> GetAllStudentAsync();
        Task<Student> GetAStudentAsync(string email);
        Task<bool> IsRollNoExists(string rollNo);
        Task<bool> IsEmailExists(string email);
    }
  
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            
            _context = context;
        }

        public async Task<Student> AddStudentAsync(Student aStudent)
        {
           await _context.Students.AddAsync(aStudent);
           await _context.SaveChangesAsync();
            return aStudent;

        }
        public async Task<Student> UpdateStudentAsync(Student aStudent)
        {
            Student student = await _context.Students.FirstOrDefaultAsync(x => x.RollNo == aStudent.RollNo);
            student.Name = aStudent.Name;
            student.Email = aStudent.Email;
            student.RollNo = aStudent.RollNo;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return aStudent;

        }
        public async Task<Student> DeleteStudentAsync(string rollNo)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.RollNo == rollNo);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetAStudentAsync(string rollN)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.RollNo == rollN);
            return student;
        }

        public async Task<bool> IsRollNoExists(string rollNo)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.RollNo == rollNo);
            if (student == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsEmailExists(string email)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Email == email);
            if (student == null)
            {
                return false;
            }
            return true;
        }
    }
}