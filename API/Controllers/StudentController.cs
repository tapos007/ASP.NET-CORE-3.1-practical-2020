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

        // [HttpGet]
        // public async Task<ActionResult> GetAll()
        // {
        //     return Ok(await _studentService.GetAllStudentAsync());
        // }

        [HttpGet]
        public ActionResult GetAll()
        {
            // // MyClass aClass = new MyClass();
            // // aClass.AddInfo("tapos");
            // // aClass.AddInfo("sumon");
            //
            // MyClassInteger aClass = new MyClassInteger();
            // aClass.AddInfo(30);
            // aClass.AddInfo(70);
            
            // MyFinalClass<string> aClass = new MyFinalClass<string>();
            // aClass.AddInfo("tapos");
            //  aClass.AddInfo("sumon");
             
             
             MyFinalClass<double> aClass = new MyFinalClass<double>();
             aClass.AddInfo(30);
             aClass.AddInfo(50);
            return Ok(aClass.GetAllData());
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