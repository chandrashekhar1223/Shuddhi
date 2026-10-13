using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shuddhi.Model;
using Shuddhi.Repository;
using System.Linq.Expressions;
using System.Text.Json;



namespace Shuddhi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PickupRequestController : Controller
    {

        private readonly IPickupRequest _PickupRequest;

        public PickupRequestController(IPickupRequest pickupRequest)
        {

            _PickupRequest = pickupRequest;
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("AddDryWaste")]
        public IActionResult AddDryWaste([FromForm] DryWaste dryWaste)
        {
            try
            {
                _PickupRequest.AddDryWaste(dryWaste);
                return Ok(new
                {
                    Message = "drywaste data added successfully",
                    UserId = dryWaste.ID,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("AddWetWaste")]
        public IActionResult AddWetWaste([FromForm] WetWaste wetwaste)
        {
            try
            {
                _PickupRequest.AddWetWaste(wetwaste);
                return Ok(new
                {
                    Message = "wetwaste data added successfully",
                    UserId = wetwaste.ID,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Microsoft.AspNetCore.Mvc.HttpPost("AddEWaste")]
        public IActionResult AddEWaste([FromForm] EWaste ewaste)
        {
            try
            {
                _PickupRequest.AddEWaste(ewaste);
                return Ok(new
                {
                    Message = "ewaste data added successfully",
                    UserId = ewaste.ID
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("AddOtherWaste")]
        public IActionResult AddOtherWaste([FromForm] OtherWaste otherwaste)
        {
            try
            {
                _PickupRequest.AddOtherWaste(otherwaste);
                return Ok(new
                {
                    Message = "other waste data added successfully",
                    UserId = otherwaste.ID
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Microsoft.AspNetCore.Mvc.HttpPost("InsertPickupRequest")]
        // [Consumes("multipart/form-data")]
        public IActionResult InsertPickupRequest([FromForm] PickupRequestModel model)
        {


            

            // List<PickupWasteModel> wasteList =JsonConvert.DeserializeObject<List<PickupWasteModel>>(model.WasteTypes);



            try
            {

                FileLogger.LogInfo(System.Text.Json.JsonSerializer.Serialize(model));


                _PickupRequest.AddPickupRequest(model);





                return Ok(new
                {
                    Message = "waste data added successfully",
                    UserId = model.UserId
                });
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred. Please try again later."
                });

            }

        }


        [HttpGet("GetPickupRequestByUserId/{userId}")]
        public ActionResult GetPickupRequestByUserId(int userId)
        {
            try
            {

                var pickupRequests = _PickupRequest.GetPickuprequestdetails1(userId);


                return Ok(pickupRequests);
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred. Please try again later."
                });
            }
        }



        [HttpGet("GetPendingPickupRequests")]
        public async Task<IActionResult> GetPendingPickupRequests()
        {
            try
            {
                var pendingRequests = await _PickupRequest.GetPendingPickupRequests();

                return Ok(pendingRequests);
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred. Please try again later."
                });
            }
        }

        [HttpPost("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] PickupRequestStatusModel requests)
        {
            try
            {
                bool result = _PickupRequest.UpdatePickupRequestStatus(
                    requests.ID,
                    requests.Status
                );

                if (result)
                {
                    return Ok(new
                    {                       
                        Message = "Status updated successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                       
                        Message = "Pickup request not found"
                    });
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);

                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new
                    {
                        Message = "An unexpected error occurred."
                    }
                );
            }


        }


        
        




    }
}
