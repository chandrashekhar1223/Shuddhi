using Shuddhi.Database;
using Shuddhi.Model;
using Shuddhi.Repository;
using System;
using System.Numerics;

namespace Shuddhi.Service
{
    public class TokenManagerService : ITokenManager
    {
        IConfiguration configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build();
        public async Task<List<TokenManagerModel>> GetRequests()
        {
            TokenManagerDB TDB = new TokenManagerDB(configuration);

            List< TokenManagerModel> requests = new List< TokenManagerModel>();

            requests = await TDB.GetTokenManage();
           


            return requests;
        }

        public async Task<List<VendorByPin>> vendorByPin(string pincode,int Wastetype_Id)
        {
            TokenManagerDB TDB = new TokenManagerDB(configuration);

            List<VendorByPin> requests = new List<VendorByPin>();

            requests =await TDB.GetVendorsByPin(pincode, Wastetype_Id);

            return requests;
        }

        public async Task<String> Ticket_Assign_Vendor(string Vendorid, string Request_Id, string R, int WasteType_Id)
        {
            IConfiguration configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build();

            TokenManagerDB TDB = new TokenManagerDB(configuration);







           string result= await TDB.Assign_To_Vendor(Vendorid, Request_Id,R,WasteType_Id);

            return result;
        }

        //        public async Task<List<PickupRequestVendorModel>> GetPickupRequestsByVendor(int vendorId)
        //        {
        //            // throw new NotImplementedException();
        //            IConfiguration configuration = new ConfigurationBuilder()
        //.SetBasePath(Directory.GetCurrentDirectory())
        //.AddJsonFile("appsettings.json")
        //.Build();
        //            string baseUrl = configuration["ApplicationSettings:BaseUrl"];

        //            // vendor.Address_ProofFileLink = $"{baseUrl}/{vendor.Address_ProofFileLink.Replace("\\", "/")}";

        //            TokenManagerDB TDB = new TokenManagerDB(configuration);

        //            List<PickupRequestVendorModel> data =new List<PickupRequestVendorModel> ();

        //             data = await TDB.GetPickupRequestsByVendor(vendorId);








        //            if (data != null)
        //            {
        //                foreach (PickupRequestVendorModel model in data)
        //                {
        //                    model.PhotoPath = model.PhotoPath!= null ? $"{baseUrl}/{model.PhotoPath.Replace("\\", "/")}" : null;
        //                }
        //            }

        //            return data;

        //        }



        public async Task<List<GroupedPickupRequestVendorModel>>
            GetPickupRequestsByVendor(int vendorId)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string baseUrl =
                configuration["ApplicationSettings:BaseUrl"] ?? "";

            TokenManagerDB TDB = new TokenManagerDB(configuration);

            List<PickupRequestVendorModel> data =
                await TDB.GetPickupRequestsByVendor(vendorId);

            // Convert photo paths before grouping
            if (data != null)
            {
                foreach (PickupRequestVendorModel model in data)
                {
                    model.PhotoPath = !string.IsNullOrWhiteSpace(model.PhotoPath)
                        ? $"{baseUrl.TrimEnd('/')}/{model.PhotoPath.Replace("\\", "/").TrimStart('/')}"
                        : null;
                }
            }

          
// Society-wise, then WasteType-wise grouping
var groupedRequests = (data ?? new List<PickupRequestVendorModel>())
    .GroupBy(x => new
    {
        x.Society_ID,
        x.Society
    })
    .Select(societyGroup => new GroupedPickupRequestVendorModel
    {
        Society_ID =(int) societyGroup.Key.Society_ID,
        Society = societyGroup.Key.Society,

        WasteTypes = societyGroup
            .GroupBy(x => x.WasteTypeId)
            .Select(wasteGroup => new GroupedWasteTypeModel
            {
                WasteTypeId =(int) wasteGroup.Key,

                PickupRequests = wasteGroup.ToList()
            })
            .ToList()
    })
    .ToList();

            return groupedRequests;

           // return groupedRequests;
        
        }
        



        public async Task<string> Status_Update(int PickupRequest_Id, int Vendor_Id, string Status)
        {

            TokenManagerDB TDB = new TokenManagerDB(configuration);
            string result = await TDB.UpdateStatus(
     PickupRequest_Id,
     Vendor_Id,
     Status);

            return result;
        }
    }
}
