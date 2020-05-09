using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CourseEnrolController : OurApplicationController
    {
        private readonly ICourseEnrollService _courseEnrollService;

        public CourseEnrolController(ICourseEnrollService courseEnrollService)
        {
            _courseEnrollService = courseEnrollService;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _courseEnrollService.FindAllAsync());
        }
        
        [HttpPost]
     //   [Authorize(Roles = "teacher, staff", Policy = "AtToken")]
        public async Task<IActionResult> Create(CourseEnrollCreateRequest request)
        {
            return Ok(await _courseEnrollService.CreateAsync(request));
        }
    }
}