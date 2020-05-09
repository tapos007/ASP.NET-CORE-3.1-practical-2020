using System.Collections.Generic;
using DLL.Model;

namespace BLL.Response
{
    public class StudentCourseReportResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string RollNo { get; set; }
        public List<CourseStudent> CourseStudents { get; set; }
    }
}