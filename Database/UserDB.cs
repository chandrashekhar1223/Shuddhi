using Shuddhi.Model;
using Shuddhi.Service;
using System;
using System.Data;
using System.Data;
using System.Data.SqlClient;

namespace Shuddhi.Database
{
    public class UserDB
    {
        private readonly string _connectionString;
        private readonly ILogger<VendorService> _logger;

        public UserDB(ILogger<VendorService> logger)
        {
            _logger = logger;
        }

        public UserDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public int InsertUserRegistration(UserRegistration user)
        {
            int insertedId = 0;
            try
            {

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertUserRegistration", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FullName", user.FullName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Gender", user.Gender);
                        cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                        cmd.Parameters.AddWithValue("@EmailID", user.EmailID);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);
                        cmd.Parameters.AddWithValue("@AddressLine1", user.AddressLine1);
                        cmd.Parameters.AddWithValue("@AddressLine2", (object)user.AddressLine2 ?? DBNull.Value);
                       // cmd.Parameters.AddWithValue("@SocietyName", user.SocietyName);
                        cmd.Parameters.AddWithValue("@AreaName", user.AreaName);
                        cmd.Parameters.AddWithValue("@CityName", user.CityName);
                        cmd.Parameters.AddWithValue("@District", user.District);
                        cmd.Parameters.AddWithValue("@PinCode", user.PinCode);
                        cmd.Parameters.AddWithValue("@Wing", user.Wing);
                       // cmd.Parameters.AddWithValue("@Sector", user.Sector);
                        //cmd.Parameters.AddWithValue("@Plot_No", user.Plot_No);
                        cmd.Parameters.AddWithValue("@House_No", user.House_No);
                        cmd.Parameters.AddWithValue("@Floor", user.FLoor);
                        cmd.Parameters.AddWithValue("@LandMark", user.Landmark);
                        cmd.Parameters.AddWithValue("@Society_Id", user.Society_Id);
                      

                        conn.Open();

                        object result = cmd.ExecuteScalar();
                        insertedId = Convert.ToInt32(result);
                    }
                }
            }
            catch(Exception ex)
            {
                //_logger.LogError("Error" + ex.Message);
                FileLogger.LogError(ex); // Log the error to a file
                 throw; // Rethrow the exception to be handled by the calling code
            }

            return insertedId;
        }

        public UserRegistration GetUser(int UserId)
        {
            int userId = 0;

           // List<string> userdetails = new List<string>();

            UserRegistration userdetails = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_getuserbyuserid", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Username", UserId);
                        conn.Open();
                        //List<string> result = cmd.ExecuteNonQuery();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Assuming you want the first column
                                userdetails = new UserRegistration
                                {
                                    ID = reader["ID"] == DBNull.Value
                ? 0
                : Convert.ToInt32(reader["ID"]),

                                    FullName = reader["FullName"] == DBNull.Value
                ? string.Empty
                : reader["FullName"].ToString(),

                                    DateOfBirth = reader["DateOfBirth"] == DBNull.Value
                ? DateTime.MinValue
                : Convert.ToDateTime(reader["DateOfBirth"]),

                                    Gender = reader["Gender"] == DBNull.Value
                ? string.Empty
                : reader["Gender"].ToString(),

                                    MobileNumber = reader["MobileNumber"] == DBNull.Value
                ? 0
                : Convert.ToInt64(reader["MobileNumber"]),

                                    EmailID = reader["EmailID"] == DBNull.Value
                ? string.Empty
                : reader["EmailID"].ToString(),

                                    AddressLine1 = reader["AddressLine1"] == DBNull.Value
                ? string.Empty
                : reader["AddressLine1"].ToString(),

                                    AddressLine2 = reader["AddressLine2"] == DBNull.Value
                ? string.Empty
                : reader["AddressLine2"].ToString(),

                                    // SocietyName = reader["SocietyName"] == DBNull.Value
                                    //     ? string.Empty
                                    //     : reader["SocietyName"].ToString(),

                                    AreaName = reader["AreaName"] == DBNull.Value
                ? string.Empty
                : reader["AreaName"].ToString(),

                                    CityName = reader["CityName"] == DBNull.Value
                ? string.Empty
                : reader["CityName"].ToString(),

                                    District = reader["District"] == DBNull.Value
                ? string.Empty
                : reader["District"].ToString(),

                                    PinCode = reader["PinCode"] == DBNull.Value
                ? 0
                : Convert.ToInt32(reader["PinCode"]),

                                    RegisteredOn = reader["RegisteredOn"] == DBNull.Value
                ? DateTime.MinValue
                : Convert.ToDateTime(reader["RegisteredOn"]),

                                    User_Profile_photo_url = reader["Profile_Photo"] == DBNull.Value
                ? string.Empty
                : reader["Profile_Photo"].ToString(),

                                    Society_Id = reader["Society_Id"] == DBNull.Value
                ? 0
                : Convert.ToInt32(reader["Society_Id"])


                                };

                                // Or by column name
                                // userdetails.Add(reader["UserName"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError("Error" + ex.Message);
                FileLogger.LogError(ex); // Log the error to a file
                throw; // Rethrow the exception to be handled by the calling code
            }
            return userdetails;
        }


        public string UpdateUserRegistration(UserRegistration UserDetails)
        {
            string message = "";

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateUserRegistration", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", UserDetails.ID);
                        cmd.Parameters.AddWithValue("@FullName", UserDetails.FullName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", (object?)UserDetails.DateOfBirth ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Gender", (object?)UserDetails.Gender ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MobileNumber", UserDetails.MobileNumber);
                        cmd.Parameters.AddWithValue("@EmailID", UserDetails.EmailID);
                       // cmd.Parameters.AddWithValue("@Password", UserDetails.Password);
                      //  cmd.Parameters.AddWithValue("@ConfirmPassword", UserDetails.ConfirmPassword);
                        cmd.Parameters.AddWithValue("@AddressLine1", (object?)UserDetails.AddressLine1 ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AddressLine2", (object?)UserDetails.AddressLine2 ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@SocietyName", (object?)UserDetails.SocietyName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AreaName", (object?)UserDetails.AreaName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CityName", (object?)UserDetails.CityName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@District", (object?)UserDetails.District ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@PinCode", (object?)UserDetails.PinCode ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@UserStatus", UserDetails.UserStatus);

                         con.Open();

                        SqlDataReader reader =  cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            message = reader["Message"].ToString();
                        }

                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return message;
        }


        public string Update_Profilephoto_UserRegistration(int id, string Profile_url)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "sp_Update_Profilephoto_UserRegistration", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@User_Id", id);
                        cmd.Parameters.AddWithValue("@Profile_Photo_Url",
                            (object)Profile_url ?? DBNull.Value);

                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }

                return "Profile photo updated successfully";
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return "Error updating profile photo: " + ex.Message;
            }
        }



    }
}
