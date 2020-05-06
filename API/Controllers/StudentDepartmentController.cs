using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StudentDepartmentController : OurApplicationController
    {
        private readonly IStudentService _studentService;

        public StudentDepartmentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _studentService.GetAllStudentDepartmentReportAsync());
        }
    }
}