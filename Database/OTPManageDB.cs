using System.Data;
using System.Data.SqlClient;

namespace Shuddhi.Database
{
    public class OTPManageDB
    {

        private readonly IConfiguration _configuration;

        public OTPManageDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> SendOTP(string mobileNumber)
        {
            // Generate 6 digit OTP
            //  Random random = new Random();
            //  string otp = random.Next(100000, 999999).ToString();

            string otp = "12345";

 

            // OTP valid for 5 minutes
            DateTime expiry = DateTime.Now.AddMinutes(5);

            string connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection connection =
                new SqlConnection(connectionString);

            using SqlCommand command =
                new SqlCommand("OTPManagement", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Flag", 1);
            command.Parameters.AddWithValue("@MobileNumber", mobileNumber);
            command.Parameters.AddWithValue("@OTP", otp);
            command.Parameters.AddWithValue("@OTPExpiry", expiry);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            int otpId = Convert.ToInt32(result);

            // TEMPORARY - only for development
            Console.WriteLine($"OTP: {otp}");

            return otpId;
        }

        public async Task<bool> VerifyOTP(
      string mobileNumber,
      string otp)
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection connection =
                new SqlConnection(connectionString);

            using SqlCommand command =
                new SqlCommand("OTPManagement", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Flag", 2);
            command.Parameters.AddWithValue("@MobileNumber", mobileNumber);
            command.Parameters.AddWithValue("@OTP", otp);

            await connection.OpenAsync();

            using SqlDataReader reader =
                await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
            {
                return false;
            }

            int otpId = Convert.ToInt32(reader["OTPId"]);

            reader.Close();

            // Mark OTP as verified
            using SqlCommand updateCommand =
                new SqlCommand("OTPManagement", connection);

            updateCommand.CommandType =
                CommandType.StoredProcedure;

            updateCommand.Parameters.AddWithValue("@Flag", 3);
            updateCommand.Parameters.AddWithValue("@OTPId", otpId);

            await updateCommand.ExecuteNonQueryAsync();

            return true;
        }

        public async Task<bool> CheckMobileNumber(string mobileNumber, int UserType)
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection connection =
                new SqlConnection(connectionString);

            using SqlCommand command =
                new SqlCommand("ForgotPasswordManagement", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserType", UserType);
            command.Parameters.AddWithValue("@Flag", 1);
            command.Parameters.AddWithValue("@MobileNumber", mobileNumber);

            await connection.OpenAsync();

            using SqlDataReader reader =
                await command.ExecuteReaderAsync();

            return await reader.ReadAsync();
        }

        public async Task<bool> ChangePassword(
    string mobileNumber,
    string newPassword,int usertype)
        {
            string connectionString =
                _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection connection =
                new SqlConnection(connectionString);

            using SqlCommand command =
                new SqlCommand("ForgotPasswordManagement", connection);

            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@UserType", usertype);
            command.Parameters.AddWithValue("@Flag", 2);
            command.Parameters.AddWithValue(
                "@MobileNumber", mobileNumber);

            command.Parameters.AddWithValue(
                "@NewPassword", newPassword);

            await connection.OpenAsync();

            using SqlDataReader reader =
                await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
            {
                return false;
            }

            int success =
                Convert.ToInt32(reader["Success"]);

            return success == 1;
        }
    }
}
