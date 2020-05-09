using BLL.Request;
using DLL.Model;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Response;
using DLL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Utility.Exceptions;

namespace BLL.Services
{
    
public interface IStudentService
    {
        Task<Student> AddStudentAsync(StudentInsertRequest request);
        Task<List<Student>> GetAllAsync();
        Task<List<StudentReport>> GetAllStudentDepartmentReportAsync();
        Task<Student> GeatAStudentAsync(string roll);
        Task<Student> UpdateAsync(string roll, StudentUpdateRequest request);
        Task<bool> DeleteAsync(string roll);

        Task<bool> IsNameExit(string name);
        Task<bool> IsRollExit(string roll);
        Task<bool> IsIdExit(int id);
        
        Task<List<StudentReportResponse>> StudentDepartmentInfoListAsync();
        Task<List<StudentCourseReportResponse>> StudentCourseEnrolledInfoListAsync();
    }

    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Student> AddStudentAsync(StudentInsertRequest request)
        {
            Student student = new Student()
            {
                Name = request.Name,
                RollNo = request.RollNo,
                Email = request.Email,
                DepartmentId = request.DepartmentId
            }; 
           await _unitOfWork.StudentRepository.CreateAsync(student);
           if (await _unitOfWork.ApplicationSaveChangesAsync())
           {
               return student;
           }
           throw new MyAppException("Student Data Not save");
        }

        public async Task<bool> DeleteAsync(string roll)
        {
            var student = await _unitOfWork.StudentRepository.GetAAsynce(x => x.RollNo == roll);
            if (student == null)
            {
                throw new MyAppException("Roll wise Student not found");
            }

            _unitOfWork.StudentRepository.DeleteAsync(student);
           if (await _unitOfWork.ApplicationSaveChangesAsync())
           {
               return true;
           }

           return false;

        }

        public async Task<List<StudentReport>> GetAllStudentDepartmentReportAsync()
        {
            var allStudent = await _unitOfWork.StudentRepository.QueryAll().Include(x=>x.Department).ToListAsync();
// sudu student
            var result = new List<StudentReport>();

            foreach (var student in allStudent)
            {
                result.Add(new StudentReport()
                {
                    StudentName = student.Name,
                    StudentEmail = student.Email,
                    DepartmentCode = student.Department.Code, // department query
                    DepartmentName = student.Department.Name // departnet query
                });
            }

            return result;
        }

        public async Task<Student> GeatAStudentAsync(string roll)
        {
            var student = await _unitOfWork.StudentRepository.GetAAsynce(x => x.RollNo == roll);
            if(student == null)
            {
                throw new MyAppException("Data Not found !!");
            }
            return student;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            var allStudent = await _unitOfWork.StudentRepository.GetListAsynce();

             if (allStudent == null)
             {
                 throw new MyAppException("No found Data");
             }

             return allStudent;
        }

        public async Task<bool> IsNameExit(string name)
        {
            var student = await _unitOfWork.StudentRepository.GetAAsynce(x => x.Name == name);
            if (student != null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> IsRollExit(string roll)
        {
            var student = await _unitOfWork.StudentRepository.GetAAsynce(x => x.RollNo == roll);
            if (student != null)
            {
                return false;
            }

            return true;
        }
        
        
        public async Task<bool> IsIdExit(int id)
        {
            var student = await _unitOfWork.StudentRepository.GetAAsynce(x => x.StudentId == id);
            if (student == null)
            {
                return false;
            }

            return true;
        }
        

        public async Task<Student> UpdateAsync(string roll, StudentUpdateRequest request)
        {

            var student = await _unitOfWork.StudentRepository.GetAAsynce(x => x.RollNo == roll);

            if (student == null)
            {
                throw new MyAppException("No found Data");
            } 

            student.Name = request.Name;
            student.RollNo = request.RollNo;
            student.Email = request.Email;
            _unitOfWork.StudentRepository.UpdateAsyc(student);
           

            if (await _unitOfWork.ApplicationSaveChangesAsync())
            {
                return student;
            }
            throw new MyAppException("Student Data Not save");
        }
        
        
        public async Task<List<StudentReportResponse>> StudentDepartmentInfoListAsync()
        {
            var students = await _unitOfWork.StudentRepository.QueryAll().Include(x => x.Department).ToListAsync();
            
            var result = new List<StudentReportResponse>();
            
            foreach (var student in students)
            {
                result.Add(new StudentReportResponse()
                {
                    Name = student.Name,
                    Email = student.Email,
                    RollNo = student.RollNo,
                    DepartmentCode = student.Department.Code,
                    DepartmentName = student.Department.Name
                });
            }

            if(result == null)
                throw new MyAppException("The student with department info. is not found!");
            return result;
        }

        public async Task<List<StudentCourseReportResponse>> StudentCourseEnrolledInfoListAsync()
        {
            var studentCourses =
                await _unitOfWork.StudentRepository.QueryAll()
                    .Include(x => x.CourseStudents)
                    .ThenInclude(x => x.Course)
                    
                    .ToListAsync();

            var result = new List<StudentCourseReportResponse>();
            
            foreach (var studentCourse in studentCourses)
            {
                result.Add(new StudentCourseReportResponse()
                {
                    Name = studentCourse.Name,
                    Email = studentCourse.Email,
                    RollNo = studentCourse.RollNo,
                    CourseStudents = studentCourse.CourseStudents.ToList()
                });
            }
            
            if(result == null)
                throw new MyAppException("The student with enrolled course info. is not found!");
            return result;
        }
    }
}
