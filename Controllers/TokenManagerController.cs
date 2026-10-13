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
        public async Task<List<VendorByPin>> GetVendorsByPincode([FromBody] VendorByPinRequest vendorbypincode)
        {

            List<VendorByPin> model = new List<VendorByPin>();

            model = await _tokenManager.vendorByPin(vendorbypincode.pincode, vendorbypincode.wasteTypeId);

            return model;

        }



        [HttpPost("Assign_Vendor")]
        public async Task<List<string>> Ticketassigntovendor(
            [FromBody] List<AssignVendor> Request)
        {
            if (Request == null || Request.Count == 0)
            {
                return new List<string> { "Request is null or empty" };
            }

            List<string> results = new List<string>();

            foreach (var item in Request)
            {
                string result = await _tokenManager.Ticket_Assign_Vendor(
                    item.Vendor_Id,
                    item.Request_Id,
                    item.pickupDateTime,
                    item.WasteType_Id
                );

                results.Add(result);
            }

            return results;
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
        public async Task<List<GroupedPickupRequestVendorModel>> GetPickupRequestsByVendor(int vendorId)
        {
            try
            {
                List<GroupedPickupRequestVendorModel> resulteddata = new List<GroupedPickupRequestVendorModel>();


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
