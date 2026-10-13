using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shuddhi.Model;
using Shuddhi.Repository;
using Shuddhi.Service;

namespace Shuddhi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : Controller
    {

        private readonly JwtTokenService _jwtTokenService;

        private readonly ILogin _login;

        IConfiguration configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build();
        private readonly IVendor _vendorService;
        //private object configuration;

        public VendorController(IVendor vendorService, JwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
            _vendorService = vendorService;
        }

        [HttpPost("vendorregister")]
        public async Task<IActionResult> Register([FromForm] VendorRegistration vendor)
        {
            try
            {

                VendorRegistration registeredVendor = await _vendorService.RegisterVendor(vendor);



                return Ok(new
                {
                    Message = "Vendor registered successfully",
                    VendorId = registeredVendor.ID,
                });
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred. Please try again later."
                });
                // return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetVendorById/{id}")]
        public async Task<IActionResult> GetVendorById(int id)
        {
            try
            {
                var vendor = await _vendorService.GetVendorById(id);

                string baseUrl = configuration["ApplicationSettings:BaseUrl"];

                // vendor.Address_ProofFileLink = $"{baseUrl}/{vendor.Address_ProofFileLink.Replace("\\", "/")}";
                vendor.Address_ProofFileLink =
     vendor.Address_ProofFileLink != null
         ? $"{baseUrl}/{vendor.Address_ProofFileLink.Replace("\\", "/")}"
         : null;


                // vendor.GSTCertificateFileLink = $"{baseUrl}/{vendor.GSTCertificateFileLink.Replace("\\", "/")}";
                vendor.GSTCertificateFileLink =
     vendor.GSTCertificateFileLink != null
         ? $"{baseUrl}/{vendor.GSTCertificateFileLink.Replace("\\", "/")}"
         : null;


                //vendor.Bank_DetailsFileLink = $"{baseUrl}/{vendor.Bank_DetailsFileLink.Replace("\\", "/")}";
                vendor.Bank_DetailsFileLink =
    vendor.Bank_DetailsFileLink != null
        ? $"{baseUrl}/{vendor.Bank_DetailsFileLink.Replace("\\", "/")}"
        : null;


                // vendor.BusinessLicenceFileLink = $"{baseUrl}/{vendor.BusinessLicenceFileLink.Replace("\\", "/")}";
                vendor.BusinessLicenceFileLink =
     vendor.BusinessLicenceFileLink != null
         ? $"{baseUrl}/{vendor.BusinessLicenceFileLink.Replace("\\", "/")}"
         : null;


                // vendor.Waste_Handling_CertificateFileLink = $"{baseUrl}/{vendor.Waste_Handling_CertificateFileLink.Replace("\\", "/")}";
                vendor.Waste_Handling_CertificateFileLink =
     vendor.Waste_Handling_CertificateFileLink != null
         ? $"{baseUrl}/{vendor.Waste_Handling_CertificateFileLink.Replace("\\", "/")}"
         : null;


                // vendor.Address_ProofFileLink = $"{baseUrl}/{vendor.Address_ProofFileLink.Replace("\\", "/")}";

                if (vendor == null)
                {
                    return NotFound(new
                    {
                        Message = "Vendor not found."
                    });
                }

                return Ok(vendor);
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An error occurred while retrieving vendor details."
                });
            }
        }



        [HttpPost("Vendorlogin")]
        public async Task<IActionResult> VendorLogin(Login request)
        {

            var result = await _vendorService.VendorLogin(request);
            try
            {

                if (result.IsValidUser == 1)
                {
                    var token = _jwtTokenService.GenerateToken(
                        result.UserId,
                        result.Mail_Id);

                    return Ok(new
                    {
                        Token = token
                    });
                }

                return Unauthorized(new
                {
                    Message = "Invalid Username or Password"


                });
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred."
                });
            }
        }


        [HttpPost("VendorUpdate")]
        public async Task<IActionResult> UpdateVendor(VendorRegistration Vendor)
        {
            try
            {
                var Vendorupdate =await _vendorService.EditVendor(Vendor);

                return Ok(new
                {
                    Message = "Vendor updated successfully",
                   
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred. Please try again later."
                });
            }

           

        }


        



    }
}
