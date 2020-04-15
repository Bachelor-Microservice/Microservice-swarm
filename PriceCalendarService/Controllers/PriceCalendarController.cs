using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PriceCalendarService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PriceCalendarController : ControllerBase
    {
        public PriceCalendarController() 
        {

        }

        [HttpGet]
        [Route("")]
        public Task<IActionResult> GetGroups() 
        {
            return Ok();
        }

        
        
        
    }
}