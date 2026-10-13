using Shuddhi.Model;
using System.Data;
using System.Data.SqlClient;

namespace Shuddhi.Database
{
    public class PickupRequestDB
    {
        private readonly string _connectionString;

        public PickupRequestDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }



        public int AddDryWaste(DryWaste model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_PickupRequest", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@WasteType", "Dry");
                cmd.Parameters.AddWithValue("@User_ID", model.User_ID);

                cmd.Parameters.AddWithValue("@Paper", (object?)model.Paper ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cardboard", (object?)model.Cardboard ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Plastic", (object?)model.Plastic ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Metal", (object?)model.Metal ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Glass", (object?)model.Glass ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Packaging_Waste", (object?)model.Packaging_Waste ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Other_Dry_Waste", (object?)model.Other_Dry_Waste ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@PhotoPath", (object?)model.Dry_Waste_Photo_Path ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedBy", (object?)model.CreatedBy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", (object?)model.UpdatedBy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);


                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int AddWetWaste(WetWaste model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_PickupRequest", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@WasteType", "Wet");
                cmd.Parameters.AddWithValue("@User_ID", model.User_ID);

                cmd.Parameters.AddWithValue("@Food_Waste", (object?)model.Food_Waste ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Vegetable_Waste", (object?)model.Vegetable_Waste ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fruit_Waste", (object?)model.Fruit_Waste ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Garden_Waste", (object?)model.Garden_Waste ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Organic_Waste", (object?)model.Organic_Waste ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Other_Wet_Waste", (object?)model.Other_Wet_Waste ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@PhotoPath", (object?)model.Wet_Waste_Photo_Path ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedBy", (object?)model.CreatedBy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", (object?)model.UpdatedBy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int AddEWaste(EWaste model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_PickupRequest", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@WasteType", "E");
                cmd.Parameters.AddWithValue("@User_ID", model.User_ID);

                cmd.Parameters.AddWithValue("@Mobile", (object?)model.Mobile ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Batteries", (object?)model.Batteries ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Charger", (object?)model.Charger ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Laptop", (object?)model.Laptop ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cable", (object?)model.Cable ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Small_Appliances", (object?)model.Small_Appliances ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Electrical_Equipment", (object?)model.Electrical_Equipment ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Electronic_Equipment", (object?)model.Electronic_Equipment ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Other_E_Waste", (object?)model.Other_E_Waste ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@PhotoPath", (object?)model.E_Waste_Photo_Path ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedBy", (object?)model.CreatedBy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", (object?)model.UpdatedBy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }
        public int AddOtherWaste(OtherWaste model)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_PickupRequest", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@WasteType", "Other");
                cmd.Parameters.AddWithValue("@User_ID", model.User_ID);

                cmd.Parameters.AddWithValue("@Clothes", (object?)model.Clothes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Shoes", (object?)model.Shoes ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Thermocol", (object?)model.Thermocol ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Used_Cooking_Oil", (object?)model.Used_Cooking_Oil ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Mixed_Waste", (object?)model.Mixed_Waste ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Other_Waste", (object?)model.Other_Waste ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@PhotoPath", (object?)model.Other_Waste_Photo_Path ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedBy", (object?)model.CreatedBy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedBy", (object?)model.UpdatedBy ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@UpdatedDate", DBNull.Value);

                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }


        public int AddUsersfilespath(string filepath, int id, string flag)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_UpdateUserDocuments", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FilePath", filepath);

                cmd.Parameters.AddWithValue("@ID", id);

                cmd.Parameters.AddWithValue("@Flag", flag);

                conn.Open();

                return Convert.ToInt32(cmd.ExecuteScalar());

            }

        }
        public string InsertPickupRequest(PickupRequestModel obj)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertPickupRequest", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", obj.UserId);
                    cmd.Parameters.AddWithValue("@Society", obj.Society);
                    cmd.Parameters.AddWithValue("@Address", obj.Address);
                    cmd.Parameters.AddWithValue("@Pincode", obj.Pincode);
                    cmd.Parameters.AddWithValue("@Note", obj.Note ?? "");
                    cmd.Parameters.AddWithValue("@Society_Id", obj.SocietyId);

                    SqlParameter p1 = cmd.Parameters.Add("@TotalEstimatedWaste", SqlDbType.Decimal);
                    p1.Precision = 10;
                    p1.Scale = 2;
                    p1.Value = obj.TotalEstimatedWaste;
                    cmd.Parameters.AddWithValue("@PickupDateTime", obj.PickupDateTime);
                    SqlParameter p2 = cmd.Parameters.Add("@Latitude", SqlDbType.Decimal);
                    p2.Precision = 10;
                    p2.Scale = 8;
                    p2.Value = obj.Latitude;

                    SqlParameter p3 = cmd.Parameters.Add("@Longitude", SqlDbType.Decimal);
                    p3.Precision = 11;
                    p3.Scale = 8;
                    p3.Value = obj.Longitude;

                    DataTable dt = new DataTable();

                    dt.Columns.Add("WasteTypeId", typeof(int));
                    dt.Columns.Add("WasteSubTypeId", typeof(int));
                    dt.Columns.Add("Quantity", typeof(decimal));
                    dt.Columns.Add("PhotoPath", typeof(string));

                    //foreach (var item in obj.WasteTypes)
                    //{
                    //    dt.Rows.Add(
                    //        item.WasteTypeId,


                    //        item.WasteSubTypeId,
                    //        item.Quantity,
                    //        item.PhotoPath
                    //        );
                    //}
                    foreach (var item in obj.WasteTypes)
                    {
                        foreach (var subTypeId in item.WasteSubTypeId)
                        {
                            dt.Rows.Add(
                                item.WasteTypeId,
                                subTypeId,
                                item.Quantity,
                                item.PhotoPath
                               
                            );
                        }
                    }

                    SqlParameter tvp = cmd.Parameters.AddWithValue("@WasteDetails", dt);
                    tvp.SqlDbType = SqlDbType.Structured;
                    tvp.TypeName = "PickupWasteType";

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        return dr["OutputStatus"].ToString();
                    }
                }
            }

            return "Failed";
        }


        public PickupRequestModel GetPickupRequestDetails(int pickupRequestId)
        {
            PickupRequestModel pickup = new PickupRequestModel();

            pickup.WasteTypes = new List<PickupWasteModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetpickupRequestDetailsbyuserid", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", pickupRequestId);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        //pickup.PickupRequestId = Convert.ToInt32(dr["PickupRequestId"]);
                        pickup.UserId = Convert.ToInt32(dr["UserId"]);
                        pickup.Address = dr["Address"].ToString();
                        pickup.Note = dr["Note"].ToString();
                        pickup.TotalEstimatedWaste = Convert.ToDecimal(dr["TotalEstimatedWaste"]);
                        pickup.PickupDateTime = Convert.ToDateTime(dr["PickupDateTime"]);
                        pickup.Latitude = Convert.ToDecimal(dr["Latitude"]);
                        pickup.Longitude = Convert.ToDecimal(dr["Longitude"]);
                        // pickup.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                    }

                    dr.NextResult();

                    while (dr.Read())
                    {
                        PickupWasteModel details = new PickupWasteModel();

                        //  details.PickupRequestDetailsId = Convert.ToInt32(dr["PickupRequestDetailsId"]);
                        details.WasteTypeId = Convert.ToInt32(dr["WasteTypeId"]);
                        // details.WasteType = dr["WasteType"].ToString();
                        //                        details.WasteSubTypeId = new List<int>
                        //{
                        //    Convert.ToInt32(dr["WasteSubTypeId"])

                       
                        
                      //details.WasteSubTypeId = dr["WasteSubTypeId"].ToString();
//};
                        // details.WasteSubType = dr["WasteSubType"].ToString();
                        details.Quantity = Convert.ToDecimal(dr["Quantity"]);
                        details.PhotoPath = dr["PhotoPath"].ToString();

                        pickup.WasteTypes.Add(details);
                    }
                }
            }

            return pickup;
        }



        public List<PickupRequestModel> GetPickupRequestDetails1(int userId)
        {
            List<PickupRequestModel> pickupRequests = new List<PickupRequestModel>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetpickupRequestDetailsbyuserid", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    // First Result Set - Pickup Requests
                    while (dr.Read())
                    {
                        PickupRequestModel pickup = new PickupRequestModel();

                        pickup.PickupRequestId = Convert.ToInt32(dr["PickupRequestId"]);
                        pickup.UserId = Convert.ToInt32(dr["UserId"]);
                        pickup.Address = dr["Address"].ToString();
                        pickup.Note = dr["Note"].ToString();
                        pickup.TotalEstimatedWaste = Convert.ToDecimal(dr["TotalEstimatedWaste"]);
                        pickup.PickupDateTime = Convert.ToDateTime(dr["PickupDateTime"]);
                        pickup.Latitude = Convert.ToDecimal(dr["Latitude"]);
                        pickup.Longitude = Convert.ToDecimal(dr["Longitude"]);

                        pickup.WasteTypes = new List<PickupWasteModel>();

                        pickupRequests.Add(pickup);
                    }

                    // Second Result Set - Waste Details
                    if (dr.NextResult())
                    {
                        while (dr.Read())
                        {
                            int pickupRequestId = Convert.ToInt32(dr["PickupRequestId"]);

                            PickupRequestModel pickup = pickupRequests
                                .FirstOrDefault(x => x.PickupRequestId == pickupRequestId);

                            if (pickup != null)
                            {
                                PickupWasteModel details = new PickupWasteModel();

                                //details.PickupRequestDetailsId = Convert.ToInt32(dr["PickupRequestDetailsId"]);
                                //details.WasteTypeId = Convert.ToInt32(dr["WasteTypeId"]);
                                //// details.WasteSubTypeId = Convert.ToInt32(dr["WasteSubTypeId"]);

                                ////                                details.WasteSubTypeId = new List<int>
                                ////{
                                ////    Convert.ToInt32(dr["WasteSubTypeId"])
                                ////};

                                //details.WasteSubTypeId1 = dr["WasteSubTypeId"].ToString();

                                //details.ID= Convert.ToInt32(dr["WasteTypeIdUnique"]);

                                //details.Quantity = Convert.ToDecimal(dr["Quantity"]);
                                //details.PhotoPath = dr["PhotoPath"].ToString();

                                details.WasteTypeId = dr["WasteTypeId"] == DBNull.Value
    ? (int?)null
    : Convert.ToInt32(dr["WasteTypeId"]);

                                details.WasteSubTypeId1 = dr["WasteSubTypeId"] == DBNull.Value
                                    ? null
                                    : dr["WasteSubTypeId"].ToString();

                                details.ID = dr["WasteTypeIdUnique"] == DBNull.Value
                                    ? 0
                                    : Convert.ToInt32(dr["WasteTypeIdUnique"]);

                                details.Quantity = dr["Quantity"] == DBNull.Value
                                    ? (decimal?)null
                                    : Convert.ToDecimal(dr["Quantity"]);

                                details.PhotoPath = dr["PhotoPath"] == DBNull.Value
                                    ? null
                                    : dr["PhotoPath"].ToString();

                                details.Status = dr["Status"] == DBNull.Value ? null : dr["Status"].ToString();

                                pickup.WasteTypes.Add(details);
                            }
                        }
                    }
                }
            }

            return pickupRequests;
        }



        public async Task<List<PickupRequestModel>> GetPendingPickupRequests()
        {
            List<PickupRequestModel> pickupRequests = new List<PickupRequestModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetPendingPickupRequests", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        await con.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                pickupRequests.Add(new PickupRequestModel
                                {
                                    PickupRequestId = Convert.ToInt32(reader["PickupRequestId"]),
                                    UserId = Convert.ToInt32(reader["UserId"]),
                                    Address = reader["Address"]?.ToString(),
                                    Note = reader["Note"]?.ToString(),
                                    TotalEstimatedWaste = Convert.ToDecimal(reader["TotalEstimatedWaste"]),
                                    PickupDateTime = Convert.ToDateTime(reader["PickupDateTime"]),
                                    Latitude = Convert.ToDecimal(reader["Latitude"]),
                                    Longitude = Convert.ToDecimal(reader["Longitude"]),
                                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                    Status = reader["Status"] == DBNull.Value
                                                ? (bool?)null
                                                : Convert.ToBoolean(reader["Status"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                throw;
            }

            return pickupRequests;
        }


        public bool UpdatePickupRequestStatus(int ID, string status)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_statusupdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@Status", status);

                        conn.Open();

                        int rowsAffected = Convert.ToInt32(cmd.ExecuteScalar());

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                throw;
            }
        }


    }
}
