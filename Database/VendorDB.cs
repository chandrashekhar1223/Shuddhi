using Shuddhi.Model;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace Shuddhi.Database
{
    public class VendorDB
    {
        private readonly string _connectionString;

        public VendorDB(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public int InsertVendor(VendorRegistration model)
        {
            int insertedId = 0;
            try
            {

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertVendorRegistration", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@VendorName", model.VendorName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Gender", model.Gender);
                        cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                        cmd.Parameters.AddWithValue("@EmailID", model.EmailID);
                        cmd.Parameters.AddWithValue("@Password", model.Password);
                        cmd.Parameters.AddWithValue("@ConfirmPassword", model.ConfirmPassword);
                        cmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
                        cmd.Parameters.AddWithValue("@AddressLine2", (object?)model.AddressLine2 ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AreaName", model.AreaName);
                        cmd.Parameters.AddWithValue("@CityName", model.CityName);
                        cmd.Parameters.AddWithValue("@District", model.District);
                        cmd.Parameters.AddWithValue("@PinCode", model.PinCode);
                        cmd.Parameters.AddWithValue("@RecycleMainID", model.RecycleMainID);
                        cmd.Parameters.AddWithValue("@RecycleSubID", model.RecycleSubID);

                        conn.Open();

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            insertedId = Convert.ToInt32(result);
                        }
                    }
                }
            }
                        catch (Exception ex)
            {
                // Log error (use logger in real project)
                //Console.WriteLine("Error occurred: " + ex.Message);
                FileLogger.LogError(ex); // Log the error to a file
            }

            return insertedId;
        }

        public int UpdateVendorDocumentLink(
    int vendorId,
    string businessLicence,
    string gstCertificate,
    string wasteHandlingCertificate,
    string addressProof,
    string bankDetails)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateVendorDocuments", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@VendorID", vendorId);

                        cmd.Parameters.AddWithValue("@BusinessLicenceFileLink",
                            (object)businessLicence ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@GstCertificateFileLink",
                            (object)gstCertificate ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@WasteHandlingCertificateFileLink",
                            (object)wasteHandlingCertificate ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@AddressProofFileLink",
                            (object)addressProof ?? DBNull.Value);

                        cmd.Parameters.AddWithValue("@BankDetailsFileLink",
                            (object)bankDetails ?? DBNull.Value);

                        con.Open();

                        int rows = Convert.ToInt32(cmd.ExecuteScalar());

                        return rows;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex); // Log the error to a file
                return 0; // Indicate failure
            }
        }
    

       public VendorRegistration GetVendor()
        {
            VendorRegistration vendordetails = null;

            return vendordetails;
        }



        public async Task<string> UpdateVendorProfile(VendorRegistration model)
        {
            string message = "";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateVendorProfile", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", model.ID);
                        cmd.Parameters.AddWithValue("@VendorName", model.VendorName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Gender", model.Gender);
                        cmd.Parameters.AddWithValue("@MobileNumber", model.MobileNumber);
                        cmd.Parameters.AddWithValue("@AddressLine1", model.AddressLine1);
                        cmd.Parameters.AddWithValue("@AddressLine2", model.AddressLine2);
                        cmd.Parameters.AddWithValue("@AreaName", model.AreaName);
                        cmd.Parameters.AddWithValue("@CityName", model.CityName);
                        cmd.Parameters.AddWithValue("@District", model.District);
                        cmd.Parameters.AddWithValue("@PinCode", model.PinCode);
                        cmd.Parameters.AddWithValue("@RecycleMainID", model.RecycleMainID);
                        cmd.Parameters.AddWithValue("@RecycleSubID", model.RecycleSubID);
                        //cmd.Parameters.AddWithValue("@BusinessLicenceFileLink", model.BusinessLicenceFileLink);
                        //cmd.Parameters.AddWithValue("@GstCertificateFileLink", model.GSTCertificateFileLink);
                        //cmd.Parameters.AddWithValue("@WasteHandlingCertificateFileLink", model.Waste_Handling_CertificateFileLink);
                        //cmd.Parameters.AddWithValue("@AddressProofFileLink", model.Address_ProofFileLink);
                        //cmd.Parameters.AddWithValue("@BankDetailsFileLink", model.Bank_DetailsFileLink);

                        con.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (await reader.ReadAsync())
                        {
                            message = reader["Message"].ToString();
                        }

                        await reader.CloseAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex); // Log the error to a file
                message = "An error occurred while updating the vendor profile.";
            }

            return message;
        }
    }
}