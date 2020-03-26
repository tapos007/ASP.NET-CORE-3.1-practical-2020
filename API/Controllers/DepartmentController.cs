using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class DepartmentController : OurApplicationController
    {
        // GET
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("hello");
        }
    }
}