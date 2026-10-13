using Shuddhi.Model;

namespace Shuddhi.Repository
{
    public interface ITokenManager
    {
        public Task<List<TokenManagerModel>> GetRequests();

        public Task<List<VendorByPin>> vendorByPin(string pincode, int Wastetype_Id);

        public Task<String> Ticket_Assign_Vendor(string Vendorid, string Request_Id,string DateTime,int WasteType_Id);

        public Task<List<GroupedPickupRequestVendorModel>> GetPickupRequestsByVendor(int vendorId);

        public Task<String> Status_Update(int PickupRequest_Id, int Vendor_Id, string Status);


       // public Task 
    }
}
