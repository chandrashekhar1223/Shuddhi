using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shuddhi.Repository;

namespace Shuddhi.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
    public class MasterController : Controller
    {

       private readonly IMaster _masterService;
        public MasterController(IMaster masterService)
        {
            _masterService = masterService;
        }
        
        [HttpGet("StateCityMaster")]
        public IActionResult GetStateCityMaster()
        {
            try
            {
                var masterData = _masterService.GetStateCityMaster();
                return Ok(masterData);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred. Please try again later."
                });
                FileLogger.LogError(ex); // Log the error to a file
            }
        }
    }
}
