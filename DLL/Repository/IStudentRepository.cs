using System.Collections.Generic;
using System.Linq;
using DLL.DbContext;
using DLL.Model;

namespace DLL.Repository
{
    public interface IStudentRepository
    {
        Student AddStudent(Student astudent);
        List<Student> GetAllStudent();
        Student GetAStudent(string email);
        
    }

    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            
            _context = context;
        }

        public Student AddStudent(Student aStudent)
        {
            _context.Students.Add(aStudent);
            _context.SaveChanges();
            return aStudent;

        }

        public List<Student> GetAllStudent()
        {
            return _context.Students.ToList();
        }

        public Student GetAStudent(string email)
        {
            var student = _context.Students.FirstOrDefault(x => x.Email == email);
            return student;
        }
    }
}