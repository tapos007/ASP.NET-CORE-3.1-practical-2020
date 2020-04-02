using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using DLL.Model;
using DLL.Repository;
using Microsoft.AspNetCore.Mvc;
using Utility.Helpers;

namespace API.Controllers
{
    
   
    public class StudentController : OurApplicationController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet]
        public async Task<ActionResult> GetAllStudnet()
        {
            return Ok(await _studentService.GetAllAsync());
        }

        [HttpGet("{roll}")]
        public async Task<ActionResult> GetAStudent(string roll)
        {
            return Ok( await _studentService.GeatAStudentAsync(roll));
        }

        [HttpPost]
        public async Task<ActionResult> AddStudent([FromForm] StudentInsertRequest request)
        {
            return Ok(await _studentService.AddStudentAsync(request));
        }

        [HttpPut("{roll}")]
        public async Task<ActionResult> Update(string roll,[FromForm]  StudentUpdateRequest request)
        {
            return Ok(await _studentService.UpdateAsync(roll, request));
        }

        [HttpDelete("{roll}")]
        public async Task<ActionResult> Delete([FromForm] string roll)
        {
            return Ok( await _studentService.DeleteAsync(roll));
        }
    }




}