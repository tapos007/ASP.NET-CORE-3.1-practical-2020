using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Utility.Helpers;

namespace API.Controllers
{
    public class TestController :  OurApplicationController
    {
        private readonly ITestService _testService;
        private readonly TaposRSA _taposRsa;

        public TestController(ITestService testService,TaposRSA taposRsa)
        {
            _testService = testService;
            _taposRsa = taposRsa;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
          //  _taposRsa.GenerateRsaKey("v1");
         // await  _testService.SaveAllData();
         await _testService.UpdateBalance();
            return Ok("hello");
        }
    }
}