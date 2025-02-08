using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PGSystem_Service.Users;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem.ResponseType;
using Microsoft.IdentityModel.Tokens;
using PGSystem_DataAccessLayer.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PGSystem.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// Đăng kí 
        /// </summary>
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<JsonResponse<string>>> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var result = await _authService.RegisterUserAsync(request);

                return Ok(new JsonResponse<string>(null, 200, "Register successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }
        /// <summary>
        /// Đăng nhập 
        /// </summary>
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<JsonResponse<LoginResponse>>> Login([FromBody] LoginRequest request)
        {
            try
            {
                var loginResponse = await _authService.LoginAsync(request);

                return Ok(new JsonResponse<LoginResponse>(loginResponse, 200, "Login successful"));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>("Something went wrong, please contact admin", 400, ex.Message));
            }
        }
        /// <summary>
        /// Tạo lại token nếu hết hạn 
        /// </summary>
        [HttpPost("Re-create-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (string.IsNullOrEmpty(request.RefreshToken))
            {
                return BadRequest("Invalid request");
            }

            try
            {
                var newAccessToken = await _authService.RefreshTokenAsync(request.RefreshToken);
                return Ok(new { Token = newAccessToken });
            }
            catch (SecurityTokenException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("login-google")]
        public IActionResult LoginWithGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = "/api/auth/google-callback" };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-callback")]
        public async Task<IActionResult> GoogleCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return BadRequest("Google Authentication Failed");

            var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
                return BadRequest("Email not found");

            var user = await _authService.CreateUser(email);
            return Ok(new { Message = "Login successful", User = user });
        }  
    }
}
