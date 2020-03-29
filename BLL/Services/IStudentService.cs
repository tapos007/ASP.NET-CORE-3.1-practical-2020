using BLL.Request;
using DLL.Model;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Utility.Exceptions;

namespace BLL.Services
{
    public interface IStudentService
    {
        Task<Student> AddStudentAsync(StudentInsertRequest request);
        Task<Student> UpdateStudentAsync(string rollNo, StudentUpdateRequest request);
        Task<Student> DeleteStudentAsync(string rollNo);
        Task<List<Student>> GetAllStudentAsync();
        Task<Student> GetAStudentAsync(string rollNo);
        Task<bool> IsEmailExists(string email);
        Task<bool> IsRollNoExits(string rollNo);
    }

    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> AddStudentAsync(StudentInsertRequest request)
        {
            Student student = new Student()
            {               
                Name = request.Name,
                Email = request.Email,
                RollNo = request.RollNo
            };
            return await _studentRepository.AddStudentAsync(student);
        }
        public async Task<Student> UpdateStudentAsync(string rollNo, StudentUpdateRequest request)
        {
            Student student = new Student()
            {
                Name = request.Name,
                Email = request.Email,
                RollNo = request.RollNo
            };
            return await _studentRepository.UpdateStudentAsync(student);
        }
        public async Task<Student> DeleteStudentAsync(string rollNo)
        {
            return await _studentRepository.DeleteStudentAsync(rollNo);
        }

        public async Task<List<Student>> GetAllStudentAsync()
        {
            return await _studentRepository.GetAllStudentAsync();
        }

        public async Task<Student> GetAStudentAsync(string rollNo)
        {
            
            var student = await  _studentRepository.GetAStudentAsync(rollNo);

            if (student == null)
            {
                throw new MyAppException("no data found");
            }

            return student;
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _studentRepository.IsEmailExists(email);
        }

        public async Task<bool> IsRollNoExits(string rollNo)
        {
            return await _studentRepository.IsRollNoExists(rollNo);
        }

    }
}
