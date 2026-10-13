using Shuddhi.Database;
using Shuddhi.Model;
using Shuddhi.Repository;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

namespace Shuddhi.Service
{
    public class PikupRequest : IPickupRequest
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        IConfiguration configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build();

        Task<DryWaste> IPickupRequest.AddDryWaste(DryWaste dryWaste)
        {
            PickupRequestDB prdb = new PickupRequestDB(configuration);
            int id = prdb.AddDryWaste(dryWaste);

            // int id = vendordb.InsertVendor(vendor);
            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "User_Documents");
            var Folder = Path.Combine(baseFolder, dryWaste.User_ID.ToString());


            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }



            var Drywastefolder = Path.Combine(Folder, "DryWasteFolder");
            if (!Directory.Exists(Drywastefolder))
            {
                Directory.CreateDirectory(Drywastefolder);
            }

            if (dryWaste.Dry_Waste_Photo != null)
            {
                var fileName = dryWaste.Dry_Waste_Photo.FileName;

                var filePath = Path.Combine(Drywastefolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    dryWaste.Dry_Waste_Photo.CopyTo(stream);
                }

                dryWaste.Dry_Waste_Photo_Path =
                    Path.Combine("User_Documents", dryWaste.User_ID.ToString(),
                    "DryWasteFolder", fileName);
            }


            prdb.AddUsersfilespath(dryWaste.Dry_Waste_Photo_Path, id, "dry");




            return Task.FromResult(dryWaste);



        }

        Task<EWaste> IPickupRequest.AddEWaste(EWaste eWaste)
        {

            PickupRequestDB prdb = new PickupRequestDB(configuration);
            int id = prdb.AddEWaste(eWaste);

            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "User_Documents");
            var Folder = Path.Combine(baseFolder, eWaste.User_ID.ToString());


            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }



            var Drywastefolder = Path.Combine(Folder, "eWaste");
            if (!Directory.Exists(Drywastefolder))
            {
                Directory.CreateDirectory(Drywastefolder);
            }

            if (eWaste.E_Waste_Photo != null)
            {
                var fileName = eWaste.E_Waste_Photo.FileName;

                var filePath = Path.Combine(Drywastefolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    eWaste.E_Waste_Photo.CopyTo(stream);
                }

                eWaste.E_Waste_Photo_Path =
                    Path.Combine("User_Documents", eWaste.User_ID.ToString(),
                    "eWaste", fileName);
            }


            prdb.AddUsersfilespath(eWaste.E_Waste_Photo_Path, id, "E");

            return Task.FromResult(eWaste);


        }

        Task<OtherWaste> IPickupRequest.AddOtherWaste(OtherWaste otherWaste)
        {
            PickupRequestDB prdb = new PickupRequestDB(configuration);
            int id = prdb.AddOtherWaste(otherWaste);

            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "User_Documents");
            var Folder = Path.Combine(baseFolder, otherWaste.User_ID.ToString());


            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }



            var Drywastefolder = Path.Combine(Folder, "otherWaste");
            if (!Directory.Exists(Drywastefolder))
            {
                Directory.CreateDirectory(Drywastefolder);
            }

            if (otherWaste.Other_Waste_Photo != null)
            {
                var fileName = otherWaste.Other_Waste_Photo.FileName;

                var filePath = Path.Combine(Drywastefolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    otherWaste.Other_Waste_Photo.CopyTo(stream);
                }

                otherWaste.Other_Waste_Photo_Path =
                    Path.Combine("User_Documents", otherWaste.User_ID.ToString(),
                    "otherWaste", fileName);
            }


            prdb.AddUsersfilespath(otherWaste.Other_Waste_Photo_Path, id, "Other");



            return Task.FromResult(otherWaste);


        }

        Task<WetWaste> IPickupRequest.AddWetWaste(WetWaste wetWaste)
        {
            PickupRequestDB prdb = new PickupRequestDB(configuration);
            int id = prdb.AddWetWaste(wetWaste);

            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "User_Documents");
            var Folder = Path.Combine(baseFolder, wetWaste.User_ID.ToString());


            if (!Directory.Exists(Folder))
            {
                Directory.CreateDirectory(Folder);
            }



            var Drywastefolder = Path.Combine(Folder, "wetwaste");
            if (!Directory.Exists(Drywastefolder))
            {
                Directory.CreateDirectory(Drywastefolder);
            }

            if (wetWaste.Wet_Waste_Photo != null)
            {
                var fileName = wetWaste.Wet_Waste_Photo.FileName;

                var filePath = Path.Combine(Drywastefolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    wetWaste.Wet_Waste_Photo.CopyTo(stream);
                }

                wetWaste.Wet_Waste_Photo_Path =
                    Path.Combine("User_Documents", wetWaste.User_ID.ToString(),
                    "wetwaste", fileName);
            }


            prdb.AddUsersfilespath(wetWaste.Wet_Waste_Photo_Path, id, "wet");




            return Task.FromResult(wetWaste);



        }


        public void addfilepath(string filepath, int id)
        {

        }

        public string AddPickupRequest(PickupRequestModel obj)
        {
            PickupRequestDB dal = new PickupRequestDB(configuration);

            return dal.InsertPickupRequest(obj);
        }
        Task<PickupRequestModel> IPickupRequest.AddPickupRequest(PickupRequestModel obj)
        {
            PickupRequestDB dal = new PickupRequestDB(configuration);
            AddFiles(obj);
            string result = dal.InsertPickupRequest(obj);




            //  obj.PickupRequestId = result;
            return Task.FromResult(obj);
        }

        public string AddFiles(PickupRequestModel obj1)
        {
            try
            {
               
                if (obj1.DryWaste_Photo != null)
                {
                    var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "User_Documents");
                    var Folder = Path.Combine(baseFolder, obj1.UserId.ToString());


                    if (!Directory.Exists(Folder))
                    {
                        Directory.CreateDirectory(Folder);
                    }



                    var Drywastefolder = Path.Combine(Folder, "DryWasteFolder");
                    if (!Directory.Exists(Drywastefolder))
                    {
                        Directory.CreateDirectory(Drywastefolder);
                    }

                    if (obj1.DryWaste_Photo != null)
                    {
                        var fileName = obj1.DryWaste_Photo.FileName;

                        var filePath = Path.Combine(Drywastefolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            obj1.DryWaste_Photo.CopyTo(stream);
                        }

                        //obj1.DryWaste_Photo =
                        //    Path.Combine("User_Documents", obj1.UserId.ToString(),
                        //    "DryWasteFolder", fileName);

                        string dryWastePath = Path.Combine(
        "User_Documents",
        obj1.UserId.ToString(),
        "DryWasteFolder",
        fileName);


                        //  obj1.WasteTypes[0].PhotoPath = dryWastePath;

                        // Find by WasteTypeId instead of array index
                        var dryWaste = obj1.WasteTypes
                            .FirstOrDefault(x => x.WasteTypeId == 1);

                        if (dryWaste != null)
                        {
                            dryWaste.PhotoPath = dryWastePath;
                        }
                        // Save dryWastePath in the database
                    }
                }


                if (obj1.WetWaste_Photo != null)
                {

                    var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "User_Documents");
                    var Folder = Path.Combine(baseFolder, obj1.UserId.ToString());


                    if (!Directory.Exists(Folder))
                    {
                        Directory.CreateDirectory(Folder);
                    }



                    var Drywastefolder = Path.Combine(Folder, "WetWasteFolder");
                    if (!Directory.Exists(Drywastefolder))
                    {
                        Directory.CreateDirectory(Drywastefolder);
                    }

                    if (obj1.DryWaste_Photo != null)
                    {
                        var fileName = obj1.WetWaste_Photo.FileName;

                        var filePath = Path.Combine(Drywastefolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            obj1.DryWaste_Photo.CopyTo(stream);
                        }

                        //obj1.DryWaste_Photo =
                        //    Path.Combine("User_Documents", obj1.UserId.ToString(),
                        //    "DryWasteFolder", fileName);

                        string wetwastepath = Path.Combine(
        "User_Documents",
        obj1.UserId.ToString(),
        "WetWasteFolder",
        fileName);

                        // Find by WasteTypeId
                        var wetWaste = obj1.WasteTypes
                            .FirstOrDefault(x => x.WasteTypeId == 2);

                        if (wetWaste != null)
                        {
                            wetWaste.PhotoPath = wetwastepath;
                        }


                        //  obj1.WasteTypes[1].PhotoPath = wetwastepath;
                        // Save dryWastePath in the database
                    }

                }


                if (obj1.EWaste_Photo != null)
                {
                    var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "User_Documents");
                    var Folder = Path.Combine(baseFolder, obj1.UserId.ToString());


                    if (!Directory.Exists(Folder))
                    {
                        Directory.CreateDirectory(Folder);
                    }



                    var Drywastefolder = Path.Combine(Folder, "EWasteFolder");
                    if (!Directory.Exists(Drywastefolder))
                    {
                        Directory.CreateDirectory(Drywastefolder);
                    }

                    if (obj1.DryWaste_Photo != null)
                    {
                        var fileName = obj1.EWaste_Photo.FileName;

                        var filePath = Path.Combine(Drywastefolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            obj1.DryWaste_Photo.CopyTo(stream);
                        }

                        //obj1.DryWaste_Photo =
                        //    Path.Combine("User_Documents", obj1.UserId.ToString(),
                        //    "DryWasteFolder", fileName);

                        string EWasteFolder = Path.Combine(
        "User_Documents",
        obj1.UserId.ToString(),
        "EWasteFolder",
        fileName);

                        // Find by WasteTypeId
                        var eWaste = obj1.WasteTypes
                            .FirstOrDefault(x => x.WasteTypeId == 3);

                        if (eWaste != null)
                        {
                            eWaste.PhotoPath = EWasteFolder;
                        }

                        // obj1.WasteTypes[2].PhotoPath = EWasteFolder;

                        // Save dryWastePath in the database
                    }

                }
                if (obj1.OtherWaste_Photo != null)
                {
                    var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "User_Documents");
                    var Folder = Path.Combine(baseFolder, obj1.UserId.ToString());


                    if (!Directory.Exists(Folder))
                    {
                        Directory.CreateDirectory(Folder);
                    }



                    var Drywastefolder = Path.Combine(Folder, "OtherWasteFolder");
                    if (!Directory.Exists(Drywastefolder))
                    {
                        Directory.CreateDirectory(Drywastefolder);
                    }

                    if (obj1.DryWaste_Photo != null)
                    {
                        var fileName = obj1.OtherWaste_Photo.FileName;

                        var filePath = Path.Combine(Drywastefolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            obj1.DryWaste_Photo.CopyTo(stream);
                        }

                        //obj1.DryWaste_Photo =
                        //    Path.Combine("User_Documents", obj1.UserId.ToString(),
                        //    "DryWasteFolder", fileName);

                        string Otherwastepath = Path.Combine(
        "User_Documents",
        obj1.UserId.ToString(),
        "OtherWasteFolder",
        fileName);

                        // Find by WasteTypeId
                        var otherWaste = obj1.WasteTypes
                            .FirstOrDefault(x => x.WasteTypeId == 4);

                        if (otherWaste != null)
                        {
                            otherWaste.PhotoPath = Otherwastepath;
                        }

                        // obj1.WasteTypes[3].PhotoPath = Otherwastepath;

                        // Save dryWastePath in the database
                    }

                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        //Task<PickupRequestModel> IPickupRequest.GetPickuprequestdetails(int pickupRequestId)
        //{
        //    PickupRequestDB dal = new PickupRequestDB(configuration);
        //    var pickupRequestDetails = dal.GetPickupRequestDetails1(pickupRequestId);
        //    try
        //    {
        //        // Get base URL (e.g. https://yourdomain.com)
        //        // string baseUrl = $"{System.Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://localhost"}";


        //        //string baseUrl = (Environment.GetEnvironmentVariable("ASPNETCORE_URLS") ?? "http://localhost")
        //        //        .Split(';')
        //        //        .FirstOrDefault(x => x.StartsWith("https://"))
        //        //     ?? "http://localhost";


        //        // var request = _httpContextAccessor?.HttpContext?.Request;


        //        // string baseUrl = $"{request.Scheme}://{request.Host}";
        //        string baseUrl = configuration["ApplicationSettings:BaseUrl"];

        //        // string baseUrl = $"{Request.Scheme}://{Request.Host}";
        //        //     PickupRequestModel model1 = new PickupRequestModel();


        //        // pickupRequestDetails.WasteTypes[0].PhotoPath = pickupRequestDetails.WasteTypes[0].PhotoPath + baseUrl;

        //        //        pickupRequestDetails.WasteTypes[0].PhotoPath =
        //        //$"{baseUrl}/{pickupRequestDetails.WasteTypes[0].PhotoPath.Replace("\\", "/")}";



        //        //foreach (var item in pickupRequestDetails[0].WasteTypes)
        //        //{
        //        //    if (!string.IsNullOrWhiteSpace(item.PhotoPath))
        //        //    {
        //        //        item.PhotoPath = $"{baseUrl}/{item.PhotoPath.Replace("\\", "/")}";
        //        //    }
        //        //}

        //        foreach (var pickup in pickupRequestDetails)
        //        {
        //            foreach (var item in pickup.WasteTypes)
        //            {
        //                if (!string.IsNullOrWhiteSpace(item.PhotoPath))
        //                {
        //                    item.PhotoPath = $"{baseUrl}/{item.PhotoPath.Replace("\\", "/")}";
        //                }
        //            }
        //        }

        //        //   //  model1.WasteTypes[1].PhotoPath = pickupRequestDetails.WasteTypes.ElementAtOrDefault(2)?.PhotoPath ?? string.Empty; // WetWaste
        //        //    // model1.WasteTypes[2].PhotoPath = pickupRequestDetails.WasteTypes.ElementAtOrDefault(3)?.PhotoPath ?? string.Empty; // EWaste
        //        //    // model1.WasteTypes[3].PhotoPath = pickupRequestDetails.WasteTypes.ElementAtOrDefault(4)?.PhotoPath ?? string.Empty; // OtherWaste

        //        //     model1.WasteTypes[0].PhotoPath =
        //        //pickupRequestDetails.WasteTypes.ElementAtOrDefault(0)?.PhotoPath != null
        //        //    ? $"{baseUrl}/{pickupRequestDetails.WasteTypes[0].PhotoPath.Replace("\\", "/")}"
        //        //    : string.Empty;

        //        //     model1.WasteTypes[1].PhotoPath =
        //        //         pickupRequestDetails.WasteTypes.ElementAtOrDefault(2)?.PhotoPath != null
        //        //             ? $"{baseUrl}/{pickupRequestDetails.WasteTypes[2].PhotoPath.Replace("\\", "/")}"
        //        //             : string.Empty;

        //        //     model1.WasteTypes[2].PhotoPath =
        //        //         pickupRequestDetails.WasteTypes.ElementAtOrDefault(3)?.PhotoPath != null
        //        //             ? $"{baseUrl}/{pickupRequestDetails.WasteTypes[3].PhotoPath.Replace("\\", "/")}"
        //        //             : string.Empty;

        //        //     model1.WasteTypes[3].PhotoPath =
        //        //         pickupRequestDetails.WasteTypes.ElementAtOrDefault(4)?.PhotoPath != null
        //        //             ? $"{baseUrl}/{pickupRequestDetails.WasteTypes[4].PhotoPath.Replace("\\", "/")}"
        //        //             : string.Empty;


        //        if (pickupRequestDetails != null)
        //        {
        //            return Task.FromResult(pickupRequestDetails);
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return Task.FromException<PickupRequestModel>(ex);
        //    }
        //    throw new NotImplementedException();
        //}



        public Task<List<PickupRequestModel>> GetPickuprequestdetails1(int userId)
        {
            PickupRequestDB dal = new PickupRequestDB(configuration);
            var pickupRequestDetails = dal.GetPickupRequestDetails1(userId);

            try
            {
                string baseUrl = configuration["ApplicationSettings:BaseUrl"];

                foreach (var pickup in pickupRequestDetails)
                {
                    if (pickup.WasteTypes == null)
                        continue;

                    foreach (var item in pickup.WasteTypes)
                    {
                        if (!string.IsNullOrWhiteSpace(item.PhotoPath))
                        {
                            item.PhotoPath = $"{baseUrl}/{item.PhotoPath.Replace("\\", "/")}";
                        }
                    }
                }

                return Task.FromResult(pickupRequestDetails);
            }
            catch (Exception ex)
            {
                return Task.FromException<List<PickupRequestModel>>(ex);
            }
        }



        public async Task<List<PickupRequestModel>> GetPendingPickupRequests()
        {
            PickupRequestDB db = new PickupRequestDB(configuration);
            //List<PickupRequestModel> pickupRequests = new List<PickupRequestModel>();
            //pickupRequests =  db.GetPendingPickupRequests();

            List<PickupRequestModel> pickupRequests = await db.GetPendingPickupRequests();



            return pickupRequests;

        }

        public bool UpdatePickupRequestStatus(int ID,string Status)
        {
            PickupRequestDB db = new PickupRequestDB(configuration);

            bool result;
            result =db.UpdatePickupRequestStatus(ID, Status);

            return result;

        }
    }
}
