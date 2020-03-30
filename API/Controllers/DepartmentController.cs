using System.Threading.Tasks;
using BLL.Request;
using BLL.Services;
using DLL.Model;
using DLL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    
    public class DepartmentController : OurApplicationController
    {
        private readonly IDepartmentService _departmentService;


        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllDepartment()
        {
            return Ok(await  _departmentService.GetAllDepartmentAsync());
        }

        [HttpGet]
        [Route(template: "{code}")]
        public  async Task<ActionResult> GetADepartment(string code)
        {
            return Ok( await  _departmentService.FindADepartmentAsync(code));
        }

        [HttpPost]
        public async Task<ActionResult> AddDepartment(DepartInsertRequest aDepartment)
        {
            
            
            return Ok( await  _departmentService.AddDepartmentAsync(aDepartment));
        }
        
        [HttpDelete("{code}")]
        public async Task<ActionResult> Delete(string code)
        {
            return Ok(await _departmentService.DeleteDepartmentAsync(code));
        }
        
        
        [HttpPut("{code}")]
        public async Task<ActionResult> Update(string code, DepertmentUpdateRequest aDepartment)
        {
            return Ok(await _departmentService.UpdateDepartmentAsync(code, aDepartment));
        }
    }
}