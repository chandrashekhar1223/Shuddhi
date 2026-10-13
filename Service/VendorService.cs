using FluentAssertions.Common;
using Microsoft.Extensions.Configuration;
using Shuddhi.Database;
using Shuddhi.Model;
using Shuddhi.Repository;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;

namespace Shuddhi.Service
{
    public class VendorService : IVendor
    {
        private readonly string _connectionString;
        IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

        public Task<VendorRegistration> RegisterVendor(VendorRegistration vendor)
        {


            try
            {


                VendorDB vendordb = new VendorDB(configuration);
                int id = vendordb.InsertVendor(vendor);
                var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Vendor_Documents");
                var vendorFolder = Path.Combine(baseFolder, id.ToString());

                // Business Licence folder inside vendor
                //#region BusinessLicense
                //var businessLicenceFolder = Path.Combine(vendorFolder, "BusinessLicence");

                //if (!Directory.Exists(businessLicenceFolder))
                //{
                //    Directory.CreateDirectory(businessLicenceFolder);
                //}

                //if (vendor.BusinessLicence != null)
                //{
                //    // Better: prevent overwrite using GUID
                //    var fileName = Guid.NewGuid() + Path.GetExtension(vendor.BusinessLicence.FileName);

                //    var filePath = Path.Combine(businessLicenceFolder, fileName);

                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        vendor.BusinessLicence.CopyToAsync(stream);
                //    }

                //    // Store relative path in DB
                //    vendor.BusinessLicenceFileLink =
                //        Path.Combine("Vendor_Documents", id.ToString(), "BusinessLicence", fileName);
                //}
                //vendordb.UpdateVendorDocumentLink(id, vendor.BusinessLicenceFileLink);
                //#endregion


                if (!Directory.Exists(vendorFolder))
                {
                    Directory.CreateDirectory(vendorFolder);
                }

                #region Business Licence

                var businessLicenceFolder = Path.Combine(vendorFolder, "BusinessLicence");

                if (!Directory.Exists(businessLicenceFolder))
                {
                    Directory.CreateDirectory(businessLicenceFolder);
                }

                if (vendor.BusinessLicence != null)
                {
                    var fileName = vendor.BusinessLicence.FileName;

                    var filePath = Path.Combine(businessLicenceFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        vendor.BusinessLicence.CopyTo(stream);
                    }

                    vendor.BusinessLicenceFileLink =
                        Path.Combine("Vendor_Documents", id.ToString(),
                        "BusinessLicence", fileName);
                }

                #endregion

                #region GST Certificate

                var gstFolder = Path.Combine(vendorFolder, "Gst_Certificate");

                if (!Directory.Exists(gstFolder))
                {
                    Directory.CreateDirectory(gstFolder);
                }

                if (vendor.GSTCertificate != null)
                {
                    var fileName = vendor.GSTCertificate.FileName;

                    var filePath = Path.Combine(gstFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        vendor.GSTCertificate.CopyTo(stream);
                    }

                    vendor.GSTCertificateFileLink =
                        Path.Combine("Vendor_Documents", id.ToString(),
                        "Gst_Certificate", fileName);
                }

                #endregion

                #region Waste Handling Certificate

                var wasteFolder = Path.Combine(vendorFolder, "Waste_Handling_Certificate");

                if (!Directory.Exists(wasteFolder))
                {
                    Directory.CreateDirectory(wasteFolder);
                }

                if (vendor.Waste_Handling_Certificate != null)
                {
                    var fileName = vendor.Waste_Handling_Certificate.FileName;

                    var filePath = Path.Combine(wasteFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        vendor.Waste_Handling_Certificate.CopyTo(stream);
                    }

                    vendor.Waste_Handling_CertificateFileLink =
                        Path.Combine("Vendor_Documents", id.ToString(),
                        "Waste_Handling_Certificate", fileName);
                }

                #endregion

                #region Address Proof

                var addressFolder = Path.Combine(vendorFolder, "Address_Proof");

                if (!Directory.Exists(addressFolder))
                {
                    Directory.CreateDirectory(addressFolder);
                }

                if (vendor.Address_Proof != null)
                {
                    var fileName = vendor.Address_Proof.FileName;

                    var filePath = Path.Combine(addressFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        vendor.Address_Proof.CopyTo(stream);
                    }

                    vendor.Address_ProofFileLink =
                        Path.Combine("Vendor_Documents", id.ToString(),
                        "Address_Proof", fileName);
                }

                #endregion

                #region Bank Details

                var bankFolder = Path.Combine(vendorFolder, "Bank_Details");

                if (!Directory.Exists(bankFolder))
                {
                    Directory.CreateDirectory(bankFolder);
                }

                if (vendor.Bank_Details != null)
                {
                    var fileName = vendor.Bank_Details.FileName;

                    var filePath = Path.Combine(bankFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        vendor.Bank_Details.CopyTo(stream);
                    }

                    vendor.Bank_DetailsFileLink =
                        Path.Combine("Vendor_Documents", id.ToString(),
                        "Bank_Details", fileName);
                }





                #endregion

                // Update DB Links
                vendordb.UpdateVendorDocumentLink(
             id,
             vendor.BusinessLicenceFileLink,
             vendor.GSTCertificateFileLink,
             vendor.Waste_Handling_CertificateFileLink,
             vendor.Address_ProofFileLink,
             vendor.Bank_DetailsFileLink);

                return Task.FromResult(new VendorRegistration { ID = id });
            }

            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                throw; // Rethrow the exception to be handled by the caller

            }
        }

        public async Task<VendorRegistration> GetVendorById(int id)
        {
            VendorRegistration vendor = null;

            using (SqlConnection con = new SqlConnection((configuration.GetConnectionString("DefaultConnection"))))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetVendorById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    await con.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            vendor = new VendorRegistration
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                VendorName = reader["VendorName"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                Gender = reader["Gender"].ToString(),
                                MobileNumber = Convert.ToInt64(reader["MobileNumber"]),
                                EmailID = reader["EmailID"].ToString(),

                                // Don't return passwords in production
                                Password = "",
                                ConfirmPassword = "",

                                AddressLine1 = reader["AddressLine1"].ToString(),
                                AddressLine2 = reader["AddressLine2"] == DBNull.Value ? null : reader["AddressLine2"].ToString(),
                                AreaName = reader["AreaName"].ToString(),
                                CityName = reader["CityName"].ToString(),
                                District = reader["District"].ToString(),
                                PinCode = Convert.ToInt32(reader["PinCode"]),

                                RecycleMainID = Convert.ToInt32(reader["RecycleMainID"]),
                                RecycleSubID = Convert.ToInt32(reader["RecycleSubID"]),

                                RegisteredOn = Convert.ToDateTime(reader["RegisteredOn"]),
                                VendorStatus = Convert.ToBoolean(reader["VendorStatus"]),

                                BusinessLicenceFileLink = reader["BusinessLicenceFileLink"] == DBNull.Value ? null : reader["BusinessLicenceFileLink"].ToString(),

                                GSTCertificateFileLink = reader["GSTCertificateFileLink"] == DBNull.Value ? null : reader["GSTCertificateFileLink"].ToString(),

                                Waste_Handling_CertificateFileLink = reader["WasteHandlingCertificateFileLink"] == DBNull.Value ? null : reader["WasteHandlingCertificateFileLink"].ToString(),

                                Address_ProofFileLink = reader["AddressProofFileLink"] == DBNull.Value ? null : reader["AddressProofFileLink"].ToString(),

                                Bank_DetailsFileLink = reader["BankDetailsFileLink"] == DBNull.Value ? null : reader["BankDetailsFileLink"].ToString()
                            };
                        }
                    }
                }
            }

            return vendor;
        }






        public async  Task<Login> VendorLogin(Login request)
        {

            Login response = new Login();
            try
            {
                using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_VendorLogin", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserId", request.UserId);
                        cmd.Parameters.AddWithValue("@Password", request.Password);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                response.IsValidUser = Convert.ToInt32(dr["IsValidUser"]);

                                if (response.IsValidUser == 1)
                                {
                                    response.UserId = dr["ID"].ToString();
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
            catch (Exception ex)
            {
                FileLogger.LogError(ex);

            }

            return response;
        }


        public async Task<string> EditVendor(VendorRegistration Vendor)
        {
            VendorDB db = new VendorDB(configuration);

          //  var update = db.UpdateVendorProfile(Vendor);


            var update = await db.UpdateVendorProfile(Vendor);



            var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "Vendor_Documents");
            var vendorFolder = Path.Combine(baseFolder, Vendor.ID.ToString());

            // Business Licence folder inside vendor
            //#region BusinessLicense
            //var businessLicenceFolder = Path.Combine(vendorFolder, "BusinessLicence");

            //if (!Directory.Exists(businessLicenceFolder))
            //{
            //    Directory.CreateDirectory(businessLicenceFolder);
            //}

            //if (vendor.BusinessLicence != null)
            //{
            //    // Better: prevent overwrite using GUID
            //    var fileName = Guid.NewGuid() + Path.GetExtension(vendor.BusinessLicence.FileName);

            //    var filePath = Path.Combine(businessLicenceFolder, fileName);

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        vendor.BusinessLicence.CopyToAsync(stream);
            //    }

            //    // Store relative path in DB
            //    vendor.BusinessLicenceFileLink =
            //        Path.Combine("Vendor_Documents", id.ToString(), "BusinessLicence", fileName);
            //}
            //vendordb.UpdateVendorDocumentLink(id, vendor.BusinessLicenceFileLink);
            //#endregion


            if (!Directory.Exists(vendorFolder))
            {
                Directory.CreateDirectory(vendorFolder);
            }

            #region Business Licence

            var businessLicenceFolder = Path.Combine(vendorFolder, "BusinessLicence");

            if (!Directory.Exists(businessLicenceFolder))
            {
                Directory.CreateDirectory(businessLicenceFolder);
            }

            if (Vendor.BusinessLicence != null)
            {
                var fileName = Vendor.BusinessLicence.FileName;

                var filePath = Path.Combine(businessLicenceFolder, fileName);


                // Delete existing file if it exists
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Vendor.BusinessLicence.CopyTo(stream);
                }

                Vendor.BusinessLicenceFileLink =
                    Path.Combine("Vendor_Documents", Vendor.ID.ToString(),
                    "BusinessLicence", fileName);
            }

            #endregion

            #region GST Certificate

            var gstFolder = Path.Combine(vendorFolder, "Gst_Certificate");

            if (!Directory.Exists(gstFolder))
            {
                Directory.CreateDirectory(gstFolder);
            }

            if (Vendor.GSTCertificate != null)
            {
                var fileName = Vendor.GSTCertificate.FileName;

                var filePath = Path.Combine(gstFolder, fileName);

                // Delete existing file if it exists
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Vendor.GSTCertificate.CopyTo(stream);
                }

                Vendor.GSTCertificateFileLink =
                    Path.Combine("Vendor_Documents", Vendor.ID.ToString(),
                    "Gst_Certificate", fileName);
            }

            #endregion

            #region Waste Handling Certificate

            var wasteFolder = Path.Combine(vendorFolder, "Waste_Handling_Certificate");

            if (!Directory.Exists(wasteFolder))
            {
                Directory.CreateDirectory(wasteFolder);
            }

            if (Vendor.Waste_Handling_Certificate != null)
            {
                var fileName = Vendor.Waste_Handling_Certificate.FileName;

                var filePath = Path.Combine(wasteFolder, fileName);


                // Delete existing file if it exists
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Vendor.Waste_Handling_Certificate.CopyTo(stream);
                }

                Vendor.Waste_Handling_CertificateFileLink =
                    Path.Combine("Vendor_Documents", Vendor.ID.ToString(),
                    "Waste_Handling_Certificate", fileName);
            }

            #endregion

            #region Address Proof

            var addressFolder = Path.Combine(vendorFolder, "Address_Proof");

            if (!Directory.Exists(addressFolder))
            {
                Directory.CreateDirectory(addressFolder);
            }

            if (Vendor.Address_Proof != null)
            {
                var fileName = Vendor.Address_Proof.FileName;

                var filePath = Path.Combine(addressFolder, fileName);



                // Delete existing file if it exists
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Vendor.Address_Proof.CopyTo(stream);
                }

                Vendor.Address_ProofFileLink =
                    Path.Combine("Vendor_Documents", Vendor.ID.ToString(),
                    "Address_Proof", fileName);
            }

            #endregion

            #region Bank Details

            var bankFolder = Path.Combine(vendorFolder, "Bank_Details");

            if (!Directory.Exists(bankFolder))
            {
                Directory.CreateDirectory(bankFolder);
            }

            if (Vendor.Bank_Details != null)
            {
                var fileName = Vendor.Bank_Details.FileName;

                var filePath = Path.Combine(bankFolder, fileName);

                // Delete existing file if it exists
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Vendor.Bank_Details.CopyTo(stream);
                }

                Vendor.Bank_DetailsFileLink =
                    Path.Combine("Vendor_Documents", Vendor.ID.ToString(),
                    "Bank_Details", fileName);
            }





            #endregion

            // Update DB Links
            db.UpdateVendorDocumentLink(
         Vendor.ID,
         Vendor.BusinessLicenceFileLink,
         Vendor.GSTCertificateFileLink,
         Vendor.Waste_Handling_CertificateFileLink,
         Vendor.Address_ProofFileLink,
         Vendor.Bank_DetailsFileLink);

          //  return Task.FromResult(new VendorRegistration { ID = Vendor.ID });





            return update;

        }
    }
}
        

