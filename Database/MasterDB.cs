using Shuddhi.Model;
using Shuddhi.Service;
using System.Data;
using System.Data.SqlClient;

namespace Shuddhi.Database
{
    public class MasterDB
    {
            private readonly string connStr;
        private readonly ILogger<VendorService> _logger;

        public MasterDB()
        {
        }
        public MasterDB(ILogger<VendorService> logger)
        {
            _logger = logger;
        }


        public MasterDB(IConfiguration configuration)
            {
            connStr = configuration.GetConnectionString("DefaultConnection");
            }
        public List<StateCity_Master> GetStateCityMaster()
        {
            List<StateCity_Master> masterList = new List<StateCity_Master>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("USP_GetCity", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                StateCity_Master master = new StateCity_Master
                                {
                                    ID = reader.GetInt64(reader.GetOrdinal("id")),   // FIX HERE
                                    State_Id = reader.GetString(reader.GetOrdinal("stateid")),
                                    City = reader.GetString(reader.GetOrdinal("city")),
                                    Pincode = reader.GetString(reader.GetOrdinal("pincode"))
                                };

                                masterList.Add(master);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log error (use logger in real project)
                //Console.WriteLine("Error occurred: " + ex.Message);
                
               
                // Optional: rethrow if you want API to fail
                throw;
                FileLogger.LogError(ex); // Log the error to a file
            }
            return masterList;
        }



    }
}
