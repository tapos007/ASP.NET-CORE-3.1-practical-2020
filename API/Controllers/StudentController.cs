using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using DLL.Model;
using DLL.Repository;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _studentService.GetAllStudentAsync());
        }

        [HttpGet]
        [Route("{rollNo}")]
        public async Task<ActionResult> GetASingleStudent(string rollNo)
        {
            return Ok(await _studentService.GetAStudentAsync(rollNo));
        }

        [HttpPost]
        public async Task<ActionResult> Insert(StudentInsertRequest aStudent)
        {
            return Ok(await _studentService.AddStudentAsync(aStudent));
        } 

        [HttpPut("{rollNo}")]
        public async Task<ActionResult> Update(string rollNo, StudentUpdateRequest aStudent)
        {
            return Ok(await _studentService.UpdateStudentAsync(rollNo, aStudent));
        }

        [HttpDelete("{rollNo}")]
        public async Task<ActionResult> Delete(string rollNo)
        {
            return Ok(await _studentService.DeleteStudentAsync(rollNo));
        }
    }




}