using System.Collections.Generic;
using System.Linq;
using DLL.Model;
using DLL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
   
    public class StudentController : OurApplicationController
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_studentRepository.GetAllStudent());
        }
        
        [HttpGet]
        [Route("{email}")]
        public ActionResult GetASingleStudent(string email)
        {
            return Ok(_studentRepository.GetAStudent(email));
        }
        
        [HttpPost]
        public ActionResult Insert([FromForm] Student aStudent)
        {
            return Ok(_studentRepository.AddStudent(aStudent));
        }
        
        // [HttpPut("{email}")]
        // public ActionResult Update(string email,[FromForm] Student aStudent)
        // {
        //     return Ok(AllStudentInfo.UpdateStudent(email,aStudent));
        // }
        //
        // [HttpDelete("{email}")]
        // public ActionResult Delete(string email)
        // {
        //     return Ok(AllStudentInfo.DeleteStudent(email));
        // }
    }

    
    
    
}