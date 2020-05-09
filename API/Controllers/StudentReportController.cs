using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StudentReportController : OurApplicationController
    {
        private readonly IStudentService _studentService;

        public StudentReportController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("student-list-with-department-info")]
        [AllowAnonymous]
        public async Task<IActionResult> StudentDepartmentInfoListAsync()
        {
            return Ok(await _studentService.StudentDepartmentInfoListAsync());
        }
        
        [HttpGet("student-list-with-course-info")]
        [AllowAnonymous]
        public async Task<IActionResult> StudentCourseEnrolledInfoListAsync()
        {
            return Ok(await _studentService.StudentCourseEnrolledInfoListAsync());
        }
    }
}