
using Shuddhi;
using Shuddhi.Database;
using Shuddhi.Model;
using System.Data;
using System.Data.SqlClient;

public class TokenManagerDB
{
    private readonly IConfiguration _configuration;


    public TokenManagerDB(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<TokenManagerModel>> GetTokenManage()
    {
        List<TokenManagerModel> result = new List<TokenManagerModel>();

        string connectionString =
            _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("TokenManager", con))
            {
                cmd.Parameters.AddWithValue("@Flag", "GetRequests");
                cmd.CommandType = CommandType.StoredProcedure;

                await con.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        TokenManagerModel model = new TokenManagerModel();

                        model.PickupRequestId = reader["PickupRequestId"] != DBNull.Value ? Convert.ToInt32(reader["PickupRequestId"]) : 0;




                        // model.Sub_Waste_Type =
                        //reader["WasteSubTypeId"] != DBNull.Value
                        //? Convert.ToInt32(reader["WasteSubTypeId"])
                        //: 0;



                        model.Sub_Waste_Type = reader["WasteSubTypeIds"] != DBNull.Value ? reader["WasteSubTypeIds"].ToString() : null;
                        //model.pickupWasteModels



                        //                    model.pickupWasteModels =
                        //reader["WasteSubTypeId"] != DBNull.Value
                        //    ? reader["WasteSubTypeId"]
                        //        .ToString()
                        //        .Split(',')
                        //        .Select(x => Convert.ToInt32(x))
                        //        .ToList()
                        //    : new List<int>();










                        model.Name = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : null;



                        model.Address =
                            reader["Address"] != DBNull.Value
                            ? reader["Address"].ToString()
                            : null;

                        model.Society = reader["Society"] != DBNull.Value
                            ? reader["Society"].ToString()
                            : null;

                        model.Note =
                            reader["Note"] != DBNull.Value
                            ? reader["Note"].ToString()
                            : null;

                        model.TotalEstimatedWaste =
                            reader["TotalEstimatedWaste"] != DBNull.Value
                            ? Convert.ToDecimal(reader["TotalEstimatedWaste"]) : 0;


                        model.Latitude =
                            reader["Latitude"] != DBNull.Value
                            ? reader["Latitude"].ToString()
                            : null;

                        model.Longitude =
                            reader["Longitude"] != DBNull.Value
                            ? reader["Longitude"].ToString()
                            : null;

                        model.Status =
                            reader["Status"] != DBNull.Value
                            ? reader["Status"].ToString()
                            : null;

                        model.QUANTITY =
                            reader["QUANTITY"] != DBNull.Value
                            ? Convert.ToDouble(reader["QUANTITY"])
                            : 0;

                        model.PhotoPath =
                            reader["PhotoPath"] != DBNull.Value
                            ? reader["PhotoPath"].ToString()
                            : null;

                        model.WasteTypeId =
                            reader["WasteTypeId"] != DBNull.Value
                            ? Convert.ToInt32(reader["WasteTypeId"])
                            : 0;

                        //model.Sub_Waste_Type =
                        //    reader["WasteSubTypeId"] != DBNull.Value
                        //    ? Convert.ToInt32(reader["WasteSubTypeId"])
                        //    : 0;

                        model.CreatedDate = reader["CreatedDate"] != DBNull.Value
                            ? reader["CreatedDate"].ToString()
                            : null;


                        model.pincode = reader["Pincode"] != DBNull.Value
                            ? reader["Pincode"].ToString()
                            : null;

                        model.Assigned_Vendor_Id = reader["Assigned_Vendor_Id"] != DBNull.Value
                            ? reader["Assigned_Vendor_Id"].ToString() : null;

                        result.Add(model);
                    }
                }
            }
        }

        return result;
    }


    public async Task<List<VendorByPin>> GetVendorsByPin(string pincode)
    {
        List<VendorByPin> vendors = new List<VendorByPin>();
        string connectionString =
         _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetVendorsByPin", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Pincode", SqlDbType.VarChar, 10).Value = pincode;

                await con.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        VendorByPin vendor = new VendorByPin();

                        vendor.VendorName = reader["VendorName"]?.ToString();



                        vendor.WasteType = reader["WasteType"]?.ToString();

                        vendor.Vendor_Id = Convert.ToInt32(reader["ID"]);

                        vendors.Add(vendor);
                    }
                }
            }
        }

        return vendors;
    }


    public async Task<String> Assign_To_Vendor(string Vendor_Id, string PickupRequest_Id, string R ,int WasteType_Id)
    {
        string Status = string.Empty;
        string connectionString =
        _configuration.GetConnectionString("DefaultConnection");
        try
        {


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("TokenManager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Flag", "AssignVendor");

                    cmd.Parameters.AddWithValue("@Vendor_Id", Vendor_Id);

                    cmd.Parameters.AddWithValue("@Pickup_Request_Id", PickupRequest_Id);

                    cmd.Parameters.AddWithValue("@PickupDateTime", R);

                    //Updating the status

                    cmd.Parameters.AddWithValue("@Status_Update", "Vendor_Assigned");

                    cmd.Parameters.AddWithValue("@WasteType_Id", WasteType_Id);

                    await con.OpenAsync();

                    int rowsAffected = await cmd.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        Status = "Ticket Assigned Successfully";
                    }
                    else
                    {
                        Status = "Pickup Request Not Found";
                    }




                }
            }
            // Status = "Ticket Assigned Successfully";
        }
        catch (Exception ex)
        {
            Status = "Something Went Wrong";
            FileLogger.LogError(ex);
        }

        return Status;
    }

    //public async Task<List<PickupRequestModel>> GetRequestdetailsforvendor(string Vendor_id)
    //{

    //    List<PickupRequestModel> result = new List<PickupRequestModel>();

    //    string connectionString =
    //        _configuration.GetConnectionString("DefaultConnection");

    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("TokenManager", con))
    //        {
    //            cmd.Parameters.AddWithValue("@Flag", "GetRequests");
    //            cmd.CommandType = CommandType.StoredProcedure;

    //            await con.OpenAsync();

    //            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
    //            {
    //                while (await reader.ReadAsync())
    //                {
    //                    TokenManagerModel model = new TokenManagerModel();

    //                    model.PickupRequestId = reader["PickupRequestId"] != DBNull.Value ? Convert.ToInt32(reader["PickupRequestId"]) : 0;


    //                    model.Sub_Waste_Type =
    //                   reader["WasteSubTypeId"] != DBNull.Value
    //                   ? Convert.ToInt32(reader["WasteSubTypeId"])
    //                   : 0;

    //                    model.Name = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : null;



    //                    model.Address =
    //                        reader["Address"] != DBNull.Value
    //                        ? reader["Address"].ToString()
    //                        : null;

    //                    model.Note =
    //                        reader["Note"] != DBNull.Value
    //                        ? reader["Note"].ToString()
    //                        : null;

    //                    model.TotalEstimatedWaste =
    //                        reader["TotalEstimatedWaste"] != DBNull.Value
    //                        ? Convert.ToDecimal(reader["TotalEstimatedWaste"]) : 0;


    //                    model.Latitude =
    //                        reader["Latitude"] != DBNull.Value
    //                        ? reader["Latitude"].ToString()
    //                        : null;

    //                    model.Longitude =
    //                        reader["Longitude"] != DBNull.Value
    //                        ? reader["Longitude"].ToString()
    //                        : null;

    //                    model.Status =
    //                        reader["Status"] != DBNull.Value
    //                        ? reader["Status"].ToString()
    //                        : null;

    //                    model.QUANTITY =
    //                        reader["QUANTITY"] != DBNull.Value
    //                        ? Convert.ToDouble(reader["QUANTITY"])
    //                        : 0;

    //                    model.PhotoPath =
    //                        reader["PhotoPath"] != DBNull.Value
    //                        ? reader["PhotoPath"].ToString()
    //                        : null;

    //                    model.WasteTypeId =
    //                        reader["WasteTypeId"] != DBNull.Value
    //                        ? Convert.ToInt32(reader["WasteTypeId"])
    //                        : 0;

    //                    model.Sub_Waste_Type =
    //                        reader["WasteSubTypeId"] != DBNull.Value
    //                        ? Convert.ToInt32(reader["WasteSubTypeId"])
    //                        : 0;

    //                    model.CreatedDate = reader["CreatedDate"] != DBNull.Value
    //                        ? reader["CreatedDate"].ToString()
    //                        : null;


    //                    model.pincode = reader["Pincode"] != DBNull.Value
    //                        ? reader["Pincode"].ToString()
    //                        : null;

    //                    model.Assigned_Vendor_Id = reader["Assigned_Vendor_Id"] != DBNull.Value
    //                        ? reader["Assigned_Vendor_Id"].ToString() : null;

    //                    result.Add(model);
    //                }
    //            }
    //        }
    //    }

    //    return result;
    //}


    public async Task<List<PickupRequestVendorModel>> GetPickupRequestsByVendor(int vendorId)
    {

        List<PickupRequestVendorModel> result = new List<PickupRequestVendorModel>();

        string connectionString =
            _configuration.GetConnectionString("DefaultConnection");
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("TokenManager", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Vendor_Id", vendorId);
                cmd.Parameters.AddWithValue("@Flag", "VendorAssignedDetail");
                await con.OpenAsync();

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(new PickupRequestVendorModel
                        {
                            PickupRequestId = reader["PickupRequestId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["PickupRequestId"]),

                            UserId = reader["UserId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["UserId"]),

                            Address = reader["Address"] == DBNull.Value
    ? null
    : reader["Address"].ToString(),

                            Society = reader["Society"] == DBNull.Value
    ? null
    : reader["Society"].ToString(),

                            Note = reader["Note"] == DBNull.Value
    ? null
    : reader["Note"].ToString(),

                            TotalEstimatedWaste = reader["TotalEstimatedWaste"] == DBNull.Value
    ? 0
    : Convert.ToDecimal(reader["TotalEstimatedWaste"]),

                            PickupDateTime = reader["PickupDateTime"] == DBNull.Value
    ? DateTime.MinValue
    : Convert.ToDateTime(reader["PickupDateTime"]),

                            Latitude = reader["Latitude"] == DBNull.Value
    ? 0
    : Convert.ToDecimal(reader["Latitude"]),

                            Longitude = reader["Longitude"] == DBNull.Value
    ? 0
    : Convert.ToDecimal(reader["Longitude"]),

                            CreatedDate = reader["CreatedDate"] == DBNull.Value
    ? DateTime.MinValue
    : Convert.ToDateTime(reader["CreatedDate"]),

                            Status = reader["Status"] == DBNull.Value
    ? null
    : reader["Status"].ToString(),

                            Pincode = reader["Pincode"] == DBNull.Value
    ? null
    : reader["Pincode"].ToString(),

                            Assigned_Vendor_Id = reader["Assigned_Vendor_Id"] == DBNull.Value
    ? null
    : Convert.ToInt32(reader["Assigned_Vendor_Id"]),

                            PickupRequestDetailId = reader["PickupRequestDetailId"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["PickupRequestDetailId"]),

                            WasteTypeId = reader["WasteTypeId"] == DBNull.Value
    ? null
    : Convert.ToInt32(reader["WasteTypeId"]),

                            WasteSubTypeId = reader["WasteSubTypeId"] == DBNull.Value
    ? null
    : reader["WasteSubTypeId"].ToString(),

                            Quantity = reader["Quantity"] == DBNull.Value
    ? 0
    : Convert.ToDecimal(reader["Quantity"]),

                            PhotoPath = reader["PhotoPath"] == DBNull.Value
    ? null
    : reader["PhotoPath"].ToString(),

                            DetailCreatedDate = reader["CreatedDate"] == DBNull.Value
    ? DateTime.MinValue
    : Convert.ToDateTime(reader["CreatedDate"]),

                            ID = reader["WasteTypeIdUnique"] == DBNull.Value
    ? 0
    : Convert.ToInt32(reader["WasteTypeIdUnique"])
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            FileLogger.LogError(ex);
        }

        return result;
    }


    public async Task<string> UpdateStatus(
     int PickupRequest_Id,
     int Vendor_Id,
     string Status)
    {
        string connectionString =
            _configuration.GetConnectionString("DefaultConnection");

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("TokenManager", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Flag", "Status_Update");
                    cmd.Parameters.AddWithValue("@Pickup_Request_Id", PickupRequest_Id);
                    cmd.Parameters.AddWithValue("@Vendor_Id", Vendor_Id);
                    cmd.Parameters.AddWithValue("@Status_Update", Status);

                    await con.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();

                    return "Status updated successfully";
                }
            }
        }
        catch (Exception ex)
        {
            FileLogger.LogError(ex);
            return "Failed to update status";
        }
    }
}