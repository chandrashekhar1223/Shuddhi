using Shuddhi.Model;
using Shuddhi.Repository;
using Shuddhi.Service;
using System.Data;
using System.Data.SqlClient;

namespace Shuddhi.Database
{
    public class LoginDB
    {
        private readonly string connStr;
        //private readonly ILogger<VendorService> _logger;
       
        public LoginDB(IConfiguration configuration)
        {
            connStr = configuration.GetConnectionString("DefaultConnection");
        }

        public Login Login(string userId, string password)
        {
            Login response = new Login();
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Login", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Password", password);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                response.IsValidUser = Convert.ToInt32(dr["IsValidUser"]);

                                if (response.IsValidUser == 1)
                                {
                                    response.UserId = dr["UserId"].ToString();
                                    response.Mail_Id = dr["EmailID"].ToString();
                                }



                            }
                            else
                            {
                                response.IsValidUser = 0;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                FileLogger.LogError(ex);

            }

            return response;
        }
    }
}
