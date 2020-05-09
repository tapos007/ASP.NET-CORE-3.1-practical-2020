using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CourseController : OurApplicationController
    {
        private readonly ICourseService _service;


        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var students = await _service.GetAllCourseAsync();
            return Ok(students);
        }



        [HttpGet]
        [Route("{code}")]
        //[Authorize(Roles = "teacher")]
        public async Task<ActionResult> GetSingleCourse(string code)
        {
            var student = await _service.FindACourseAsync(code);
            return Ok(student);
        }

        [HttpPost]
        //[Authorize(Roles = "teacher")]
        public async Task<ActionResult> Insert(CourseInsertRequest course)
        {
            //StudentRequest students = new StudentRequest();
            var courses = await _service.AddCourseAsync(course);
            return Ok(courses);
        }

        [HttpPut("{code}")]
        //[Authorize(Roles = "staff")]
        public async Task<ActionResult> Update(string code, CourseInsertRequest request)
        {
            return Ok(await _service.UpdateCourseAsync(code, request));
        }

        [HttpDelete("{code}")]
        //[Authorize(Roles = "teacher")]
        public async Task<ActionResult> Delete([FromForm] string code)
        {
            return Ok(await _service.DeleteCourseAsync(code));
        }
    }
}