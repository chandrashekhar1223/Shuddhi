using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shuddhi.Service;

namespace Shuddhi.Controllers
{
   
    public class GetWasteMasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        [Route("api/Waste/GetWasteMaster")]
        public IActionResult GetWasteMaster()
        {
            try
            {
               // WasteBAL objBAL = new WasteBAL();
                WasteMaster objBAL = new WasteMaster();

                var result = objBAL.GetWasteMaster();

                return Ok(result);
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return ex.Message switch
                {
                    "No data found" => NotFound(new { message = ex.Message }),
                    _ => StatusCode(500, new { message = "An error occurred while processing your request." })
                };
            }
        }
    }
}
