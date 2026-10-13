using Shuddhi.Model;
using Shuddhi.Service;
using System.Data;
using System.Data.SqlClient;

namespace Shuddhi.Database
{
    public class DashboardDB
    {

        private readonly string connStr;
        private readonly ILogger<VendorService> _logger;


        public DashboardDB(IConfiguration configuration)
        {
            connStr = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<DashboardModel?> GetDashboard(string userId)
        {
            DashboardModel dashboardModel =new DashboardModel();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("Dashboard", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    await con.OpenAsync();
                    using (SqlDataReader dr =await  cmd.ExecuteReaderAsync())
                    {
                        if (await dr.ReadAsync())
                        {
                            // response.IsValidUser = Convert.ToInt32(dr["IsValidUser"]);


                            dashboardModel.TotalWasteKg = Convert.ToDecimal(dr["TotalWasteKg"]);
                            dashboardModel.CO2SavedKg = Convert.ToDecimal(dr["CO2SavedKg"]);
                            dashboardModel.TotalPoints = Convert.ToDecimal(dr["TotalPoints"]);
                            

                            

                        }
                        else
                        {
                          
                        }
                    }
                }
            }
            return dashboardModel;
        }
    }
}
