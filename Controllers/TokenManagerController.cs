using Microsoft.AspNetCore.Mvc;
using Shuddhi.Model;
using Shuddhi.Repository;

namespace Shuddhi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenManagerController : Controller
    {
        private readonly ITokenManager _tokenManager;
        public TokenManagerController(ITokenManager tokenmanager)
        {
            _tokenManager = tokenmanager;
        }


        [HttpGet("GetRequests")]
        public async Task<List<TokenManagerModel>>  GetRequests()
        {
           List< TokenManagerModel> model = new List< TokenManagerModel>();

           model= await _tokenManager.GetRequests();

            return model;
        }


        [HttpPost("GetVendor")]
        public async Task<List<VendorByPin>> GetVendorsByPincode([FromBody] string pincode)
        {

            List<VendorByPin> model = new List<VendorByPin>();

            model = await _tokenManager.vendorByPin(pincode);

            return model;

        }

        [HttpPost("Assign_Vendor")]
        public async Task<string> Ticketassigntovendor([FromBody] AssignVendor Request)
        {

            if (Request == null)
            {
                return "Request is null";
            }
            string result = await _tokenManager.Ticket_Assign_Vendor(Request.Vendor_Id, Request.Request_Id,Request.pickupDateTime,Request.WasteType_Id);
            return result;
        }

        //[HttpPost("Ticket_Details")]
        //public async Task<TokenManagerModel> Ticket_Details()
        //{


        //    return "";
        //}

        //[HttpGet("GetTicketDetails")]
        //public async Task<string> TicketDetails()
        //{
        //    string result = await 
        //}

        [HttpPost("GetAssignedTicketDetails")]
        public async Task<List<PickupRequestVendorModel>> GetPickupRequestsByVendor(int vendorId)
        {
            try
            {
                List<PickupRequestVendorModel> resulteddata = new List<PickupRequestVendorModel>();


                resulteddata = await _tokenManager
                    .GetPickupRequestsByVendor(vendorId);

                return resulteddata;
            }
            catch(Exception ex)
            {
                FileLogger.LogError(ex);
                throw;
            }
            
           
        }

        [HttpPost("UpdateStatus")]
        public async Task<string> Update_Status(int PickupRequest_Id,int Vendor_Id,string Status)
        {
           string result= await _tokenManager.Status_Update(PickupRequest_Id, Vendor_Id, Status);

            return result;

        }
















    }
}
