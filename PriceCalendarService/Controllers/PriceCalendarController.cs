using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace PriceCalendarService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PriceCalendarController : ControllerBase
    {

        public PriceCalendarController()
        {
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get() 
        {
            return Ok("");
        }

        [HttpGet("GetSingle")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> getInInterval() 
        {
            return null;
        }

        [HttpPut]
        public async Task<IActionResult> Update()
        {
            return null;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return null;
        }
    
    }
}
