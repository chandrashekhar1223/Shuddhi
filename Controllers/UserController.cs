using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shuddhi.Database;
using Shuddhi.Model;
using Shuddhi.Repository;
using Shuddhi.Service;

namespace Shuddhi.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUser _userService;
        IConfiguration configuration = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json")
.Build();

        IConfiguration configuration1 = new ConfigurationBuilder()
 .SetBasePath(Directory.GetCurrentDirectory())
 .AddJsonFile("appsettings.json")
 .Build();

        public UserController(IUser userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserRegistration user)
        {
            try
            {
                UserRegistration registeredUser = await _userService.RegisterUser(user);
                return Ok(new
                {
                    Message = "User registered successfully",
                    UserId = registeredUser.ID,
                });
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred. Please try again later."
                });
                
            }
        }
        
       
        [HttpGet("getusers/{Userid}")]
        public async Task<IActionResult> GetUsers(int Userid)
        {
            string baseUrl = configuration["ApplicationSettings:BaseUrl"];
            try
            {
                var user = await _userService.GetUser(Userid);
                user.User_Profile_photo_url =
   user.User_Profile_photo_url!= null
       ? $"{baseUrl}/{user.User_Profile_photo_url.Replace("\\", "/")}"
       : null;
                return Ok(user);
            }
            catch (Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred. Please try again later."
                });
               
                // FileLogger.LogError(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("EditUser")]
        public async Task<IActionResult> EditUser([FromForm] UserRegistration UserDetails)
        {
            try
            {
                if (UserDetails != null)
                {
                    #region Profile Photo
                    var baseFolder = Path.Combine(Directory.GetCurrentDirectory(), "User_Documents");
                    var vendorFolder = Path.Combine(baseFolder, UserDetails.ID.ToString());

                    if (!Directory.Exists(vendorFolder))
                    {
                        Directory.CreateDirectory(vendorFolder);
                    }

                    var businessLicenceFolder = Path.Combine(vendorFolder, "Profile_Photo");

                    if (!Directory.Exists(businessLicenceFolder))
                    {
                        Directory.CreateDirectory(businessLicenceFolder);
                    }

                    if (UserDetails.User_Profile_Photo != null)
                    {
                        var fileName = UserDetails.User_Profile_Photo.FileName;

                        var filePath = Path.Combine(businessLicenceFolder, fileName);


                        // Delete existing file if it exists
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            UserDetails.User_Profile_Photo.CopyTo(stream);
                        }

                        UserDetails.User_Profile_photo_url =
                            Path.Combine("User_Documents", UserDetails.ID.ToString(),
                            "Profile_Photo", fileName);

                        UserDB userdb= new UserDB(configuration1);
                        userdb.Update_Profilephoto_UserRegistration(UserDetails.ID, UserDetails.User_Profile_photo_url);
                    }

                    #endregion


                }
                var  user =  _userService.EditUser(UserDetails);
                // return Ok(user);

                return Ok(new
                {
                    status = true,
                    message = user
                });
            }
            catch(Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred. Please try again later."
                });
            }
        }


      



    }
}
