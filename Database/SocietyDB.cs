using Shuddhi.Model;
using System.Data;
using System.Data.SqlClient;

namespace Shuddhi.Database
{
    public class SocietyDB
    {

        private readonly string _connectionString;

        public SocietyDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddSociety(AddSociety model)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Society", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Society_Name",
                        (object?)model.Society_Name ?? DBNull.Value);
                   

                    cmd.Parameters.AddWithValue("@Pincode",
                        (object?)model.Pincode ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@Landmark",
                        (object?)model.Landmark ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@Sector",
                        (object?)model.Sector ?? DBNull.Value);

                    cmd.Parameters.AddWithValue("@Plot_No",
                        (object?)model.Plot_No ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Flag", "Insert");


                    await con.OpenAsync();

                    object result = await cmd.ExecuteScalarAsync();

                    return Convert.ToInt32(result);
                }
            }
        }

        public async Task<List<AddSociety>> GetSocieties(string search)
        {
            List<AddSociety> societies = new List<AddSociety>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "sp_Society", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@Search",
                        string.IsNullOrWhiteSpace(search)
                            ? DBNull.Value
                            : search);

                    cmd.Parameters.AddWithValue("@Flag", "Search");

                    await con.OpenAsync();

                    using (SqlDataReader dr =
                           await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            societies.Add(new AddSociety
                            {
                                Id = Convert.ToInt32(dr["Id"]),

                                Society_Name =
                                    dr["Society_Name"] == DBNull.Value
                                    ? null
                                    : dr["Society_Name"].ToString(),

                                Pincode =
                                    dr["Pincode"] == DBNull.Value
                                    ? null
                                    : dr["Pincode"].ToString(),

                                Landmark =
                                    dr["Landmark"] == DBNull.Value
                                    ? null
                                    : dr["Landmark"].ToString(),

                                Sector =
                                    dr["Sector"] == DBNull.Value
                                    ? null
                                    : dr["Sector"].ToString(),

                                Plot_No =
                                    dr["Plot_No"] == DBNull.Value
                                    ? null
                                    : dr["Plot_No"].ToString(),

                                Created_Date =
                                    dr["Created_Date"] == DBNull.Value
                                    ? null
                                    : 
                                        dr["Created_Date"].ToString()
                            });
                        }
                    }
                }
            }

            return societies;
        }


        public async Task<List<AddSociety>> GetSocietyList()
        {
            List<AddSociety> societies = new List<AddSociety>();

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(
                    "sp_Society", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                   

                    cmd.Parameters.AddWithValue("@Flag", "Get");

                    await con.OpenAsync();

                    using (SqlDataReader dr =
                           await cmd.ExecuteReaderAsync())
                    {
                        while (await dr.ReadAsync())
                        {
                            societies.Add(new AddSociety
                            {
                                Id = Convert.ToInt32(dr["Id"]),

                                Society_Name =
                                    dr["Society_Name"] == DBNull.Value
                                    ? null
                                    : dr["Society_Name"].ToString(),

                                Pincode =
                                    dr["Pincode"] == DBNull.Value
                                    ? null
                                    : dr["Pincode"].ToString(),

                                Landmark =
                                    dr["Landmark"] == DBNull.Value
                                    ? null
                                    : dr["Landmark"].ToString(),

                                Sector =
                                    dr["Sector"] == DBNull.Value
                                    ? null
                                    : dr["Sector"].ToString(),

                                Plot_No =
                                    dr["Plot_No"] == DBNull.Value
                                    ? null
                                    : dr["Plot_No"].ToString(),

                                Created_Date =
                                    dr["Created_Date"] == DBNull.Value
                                    ? null
                                    :
                                        dr["Created_Date"].ToString()
                            });
                        }
                    }
                }
            }

            return societies;
        }

        public async Task<(bool Success, string Message)> UpdateSociety(AddSociety society)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Society", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", society.Id);
                    cmd.Parameters.AddWithValue("@Society_Name",
                        (object)society.Society_Name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Pincode",
                        (object)society.Pincode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Landmark",
                        (object)society.Landmark ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Sector",
                        (object)society.Sector ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Plot_No",
                        (object)society.Plot_No ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Flag", "Update");

                    await con.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            bool success = Convert.ToBoolean(reader["Success"]);
                            string message = reader["Message"].ToString();

                            return (success, message);
                        }
                    }
                }
            }

            return (false, "Unable to update society.");
        }


        public async Task<object> DeleteSociety(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Society", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Flag", "Delete");

                        await con.OpenAsync();

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new
                                {
                                    Success = Convert.ToInt32(reader["Success"]),
                                    Message = reader["Message"]?.ToString()
                                };
                            }
                        }
                    }
                }

                return new
                {
                    Success = 0,
                    Message = "Unable to delete society."
                };
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);

                return new
                {
                    Success = 0,
                    Message = "An error occurred while deleting society."
                };
            }
        }


        public async Task<List<AddSociety>> GetSocietyListByPincode(GetSocieties pincode)
        {
            List<AddSociety> societies = new List<AddSociety>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(
                        "sp_Society", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;



                        cmd.Parameters.AddWithValue("@Flag", "Pincodewise_Societies");

                        cmd.Parameters.AddWithValue("@Pincode", pincode.Pincode.ToString());

                        await con.OpenAsync();

                        using (SqlDataReader dr =
                               await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                societies.Add(new AddSociety
                                {
                                    Id = Convert.ToInt32(dr["Id"]),

                                    Society_Name =
                                        dr["Society_Name"] == DBNull.Value
                                        ? null
                                        : dr["Society_Name"].ToString(),

                                    Pincode =
                                        dr["Pincode"] == DBNull.Value
                                        ? null
                                        : dr["Pincode"].ToString(),

                                    Landmark =
                                        dr["Landmark"] == DBNull.Value
                                        ? null
                                        : dr["Landmark"].ToString(),

                                    Sector =
                                        dr["Sector"] == DBNull.Value
                                        ? null
                                        : dr["Sector"].ToString(),

                                    Plot_No =
                                        dr["Plot_No"] == DBNull.Value
                                        ? null
                                        : dr["Plot_No"].ToString(),

                                    Created_Date =
                                        dr["Created_Date"] == DBNull.Value
                                        ? null
                                        :
                                            dr["Created_Date"].ToString()
                                });
                            }
                        }
                    }
                }

                return societies;
            }
            catch(Exception ex)
            {
                FileLogger.LogError(ex);

                throw;
            }
        }


        public async Task<SocietyModel?> GetSocietyById(int societyId)
        {
            SocietyModel? society = null;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Society", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Flag", SqlDbType.VarChar, 50)
                        .Value = "GetById";

                    cmd.Parameters.Add("@Society_Id", SqlDbType.Int)
                        .Value = societyId;

                    await con.OpenAsync();

                    using (SqlDataReader reader =
                           await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            society = new SocietyModel
                            {
                                Id = reader["Id"] == DBNull.Value
                                    ? 0
                                    : Convert.ToInt32(reader["Id"]),

                                Society_Name = reader["Society_Name"] == DBNull.Value
                                    ? string.Empty
                                    : reader["Society_Name"].ToString(),

                                Pincode = reader["Pincode"] == DBNull.Value
                                    ? string.Empty
                                    : reader["Pincode"].ToString(),

                                Landmark = reader["Landmark"] == DBNull.Value
                                    ? string.Empty
                                    : reader["Landmark"].ToString(),

                                Sector = reader["Sector"] == DBNull.Value
                                    ? string.Empty
                                    : reader["Sector"].ToString(),

                                Plot_No = reader["Plot_No"] == DBNull.Value
                                    ? string.Empty
                                    : reader["Plot_No"].ToString(),

                                Created_Date = reader["Created_Date"] == DBNull.Value
                                    ? null
                                    : Convert.ToDateTime(reader["Created_Date"])
                            };
                        }
                    }
                }
            }

            return society;
        }

    }
}
