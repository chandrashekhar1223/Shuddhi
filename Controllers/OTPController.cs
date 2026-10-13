using Microsoft.AspNetCore.Mvc;
using Shuddhi.Model;
using Shuddhi.Repository;
using System.Reflection;

namespace Shuddhi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OTPController : Controller
    {
        private readonly IOTPService oTPService;

        public OTPController(IOTPService oTPService)
        {
            this.oTPService = oTPService;
        }

      
        [HttpPost("Sendotp")]
        public async Task<IActionResult> Sendotp(SendOTPModel sendotp)
        {
            try
            {
                if (sendotp == null || string.IsNullOrWhiteSpace(sendotp.MobileNumber))
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Mobile number is required"
                    });
                }
                int otpId = await oTPService.SendOTP(sendotp.MobileNumber);

                return Ok(new
                {
                    Success = true,
                    Message = "OTP generated successfully",
                    OTPId = otpId
                });

            }
          
              
            
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.Message
                });

                
            }
           
        }

        [HttpPost("Verifyotp")]
        public async Task<IActionResult> Verifyotp(VerifyOTPModel verifyOTPModel)
        {
            try
            {
                if (verifyOTPModel == null ||
                    string.IsNullOrWhiteSpace(verifyOTPModel.MobileNumber))
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Mobile number is required"
                    });
                }

                if (string.IsNullOrWhiteSpace(verifyOTPModel.OTP))
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "OTP is required"
                    });
                }

                bool result = await oTPService.VerifyOTP(
                    verifyOTPModel.MobileNumber,
                    verifyOTPModel.OTP
                );

                if (result)
                {
                    return Ok(new
                    {
                        Success = true,
                        Message = "OTP verified successfully"
                    });
                }

                return BadRequest(new
                {
                    Success = false,
                    Message = "Invalid or expired OTP"
                });
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.Message
                });
               
            }
        }


        [HttpPost("ForgotPasswordSendOTP")]
        public async Task<IActionResult> ForgotPasswordSendOTP(
    ForgotPasswordModel model)
        {
            try
            {
                if (model == null ||
                    string.IsNullOrWhiteSpace(model.MobileNumber))
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Mobile number is required"
                    });
                }

                // Check user exists
                bool userExists =
                    await oTPService.CheckMobileNumber(
                        model.MobileNumber, model.UserType);

                if (!userExists)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Mobile number is not registered"
                    });
                }

                // Generate OTP
                int otpId =
                    await oTPService.SendOTP(
                        model.MobileNumber);

                return Ok(new
                {
                    Success = true,
                    Message = "OTP sent successfully",
                    OTPId = otpId
                });
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.Message
                });
               
            }
        }

        [HttpPost("ForgotPasswordVerifyOTP")]
        public async Task<IActionResult> ForgotPasswordVerifyOTP(
    VerifyOTPModel model)
        {
            try
            {
                if (model == null ||
                    string.IsNullOrWhiteSpace(model.MobileNumber))
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Mobile number is required"
                    });
                }

                if (string.IsNullOrWhiteSpace(model.OTP))
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "OTP is required"
                    });
                }

                bool result =
                    await oTPService.VerifyOTP(
                        model.MobileNumber,
                        model.OTP);

                if (!result)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Invalid or expired OTP"
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Message = "OTP verified successfully"
                });
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }


        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(
    ChangePasswordModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Invalid request"
                    });
                }

                if (string.IsNullOrWhiteSpace(model.MobileNumber))
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Mobile number is required"
                    });
                }

                if (string.IsNullOrWhiteSpace(model.NewPassword))
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "New password is required"
                    });
                }

                if (string.IsNullOrWhiteSpace(model.ConfirmPassword))
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Confirm password is required"
                    });
                }

                if (model.NewPassword != model.ConfirmPassword)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "New password and confirm password do not match"
                    });
                }

                bool result =
                    await oTPService.ChangePassword(
                        model.MobileNumber,
                        model.NewPassword,model.usertype);

                if (!result)
                {
                    return BadRequest(new
                    {
                        Success = false,
                        Message = "Unable to change password"
                    });
                }

                return Ok(new
                {
                    Success = true,
                    Message = "Password changed successfully"
                });
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(500, new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }

    }
}
