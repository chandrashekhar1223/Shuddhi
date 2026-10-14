using Microsoft.AspNetCore.Mvc;
using Shuddhi.Model;
using Shuddhi.Repository;

namespace Shuddhi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class SocietyController : Controller
    {
        private readonly ISocietyService _societyService;

        public SocietyController(ISocietyService societyService)
        {
            _societyService = societyService;
        }

        [HttpPost("AddSociety")]
        public async Task<IActionResult> AddSociety([FromBody] AddSociety model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest("Invalid society data.");
                }

                int societyId = await _societyService.AddSociety(model);

                return Ok(new
                {
                    Success = true,
                    Message = "Society added successfully.",
                    Id = societyId
                });
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);

                return StatusCode(500, new
                {
                    Success = false,
                    Message = "An error occurred while adding society."
                });
            }
        }


        [HttpPost("Search")]
        public async Task<IActionResult> SearchSociety([FromBody] string Search)
        {
            try
            {
                List<AddSociety> list = await _societyService.GetSocieties(Search);
               // _societyService.GetSocieties(Search);

                return Ok(list);

            }
            catch (Exception ex)
            {

                FileLogger.LogError(ex);

                return StatusCode(500, new
                {
                    message = "An error occurred while searching societies.",
                    error = ex.Message
                });
            }


        }



        [HttpGet("GetSocietes")]
        public async Task<IActionResult> GetSocieties()
        {
            try
            {
                List<AddSociety> list = await _societyService.GetSocietieslist();

                return Ok(list);
            }
            catch(Exception ex)
            {
                FileLogger.LogError(ex);

                return StatusCode(500, new
                {
                    message = "An error occurred while searching societies.",
                    error = ex.Message
                });

            }
        }

        [HttpPost("UpdateSocieties")]
        public async Task<(bool, string)> UpdateSociety(AddSociety model)
        {

            try
            {
               var result =await _societyService.UpdateSociety(model);

                return result;


            }
            catch(Exception ex)
            {
                FileLogger.LogError(ex);

                return (false, "An error occurred while updating society.");

            }
        }


        [HttpDelete("DeleteSociety/{id}")]
        public async Task<IActionResult> DeleteSociety(int id)
        {
            try
            {
                var result = await _societyService.DeleteSociety(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);

                return StatusCode(500, new
                {
                    Success = 0,
                    Message = "An error occurred while deleting society."
                });
            }
        }


        [HttpPost("GetSocietes")]
        public async Task<IActionResult> GetSocietiesByPincode([FromBody] GetSocieties Pincode)
        {
            try
            {
                List<AddSociety> list = await _societyService.GetSocietyListByPincode(Pincode);

                return Ok(list);
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);

                return StatusCode(500, new
                {
                    message = "An error occurred while searching societies.",
                    error = ex.Message
                });

            }
        }



    }
}
