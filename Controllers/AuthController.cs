using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Shuddhi.Model;
using Shuddhi.Repository;

namespace Shuddhi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        
        private readonly JwtTokenService _jwtTokenService;

        private readonly ILogin _login;

        public AuthController(JwtTokenService jwtTokenService, ILogin login)
        {
            _jwtTokenService = jwtTokenService;
            _login = login;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(Login request)
        {

            Login result = await _login.Login(request);
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
            catch(Exception ex)
            {
                FileLogger.LogError(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "An unexpected error occurred."
                });
            }
        }
    }
}
