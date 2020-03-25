using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(AllStudentInfo.GetAllStudent());
        }
        
        [HttpGet]
        [Route("{email}")]
        public ActionResult GetASingleStudent(string email)
        {
            return Ok(AllStudentInfo.GetAStudent(email));
        }
        
        [HttpPost]
        public ActionResult Insert([FromForm] Student aStudent)
        {
            return Ok(AllStudentInfo.AddStudent(aStudent));
        }
        
        [HttpPut("{email}")]
        public ActionResult Update(string email,[FromForm] Student aStudent)
        {
            return Ok(AllStudentInfo.UpdateStudent(email,aStudent));
        }
        
        [HttpDelete("{email}")]
        public ActionResult Delete(string email)
        {
            return Ok(AllStudentInfo.DeleteStudent(email));
        }
    }

    public class Student
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    
    public static class  AllStudentInfo{
        public static List<Student> Students { get; set; } = new List<Student>()
        {
            new Student()
            {
                Email = "tapos.aa@gmail.com",
                Name = "tapos"
            },
            new Student()
            {
                Email = "subir@gmail.com",
                Name = "subir"
            }
        };
        
        public static List<Student> GetAllStudent()
        {
            return Students; 
        }

        public static Student AddStudent(Student aStudent)
        {
            Students.Add(aStudent);
            return aStudent;

        }

        public static Student GetAStudent(string email)
        {
            return Students.FirstOrDefault(x => x.Email == email);
        }

        public static Student UpdateStudent(string email, Student aStudent)
        {
            foreach (var student in Students)
            {
                if (student.Email == email)
                {
                    student.Email = aStudent.Email;
                    student.Name = aStudent.Name;
                }
            }

            return aStudent;
        }
        
        public static Student DeleteStudent(string email)
        {
            var student = Students.FirstOrDefault(x => x.Email == email);
            var studentList = Students.Where(x => x.Email != email).ToList();
            Students = studentList;

            return student;

        }
    }
}