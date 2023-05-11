using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentsApi.Dataccess.Repository.IRepository;
using StudentsAsp.Utility;
using StudentsApi.Model.ViewModel;
using StudentsApi.Model;
using StudentsApi.Dataccess.TokenService;
using Refit;
using Microsoft.AspNetCore.Authorization;
using Octokit;
using System.IdentityModel.Tokens.Jwt;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;
using System.Security.Claims;
using StudentsApi.Dataccess.Data;

namespace StudentsApiWeb.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }
    

        [HttpGet]
        [Route("[controller]/getRoles")]
        public async Task<IActionResult> getRoles()
        {
            var role = _roleManager.Roles.ToList();
            return Ok(role);
        }
        [HttpGet]
        [Route("[controller]/getUsers")]

        public async Task<IActionResult> getUsers()
        {
            var user = _userManager.Users.ToList();
            return Ok(user);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] loginUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tokenService.LoginAsync(model);

            if (!result.isAuthenticated)
                return BadRequest(result.message);

            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var appid = new appId();
            appid.ApplicationUserId = claims.Value;
            return Ok(appid);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] registerUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _tokenService.RegisterAsync(model);
            if (!result.isAuthenticated)
                return BadRequest(result.message);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }
        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _tokenService.RefreshTokenAsync(refreshToken);

            if (!result.isAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }
        [HttpPost("addRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _tokenService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeToken model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await _tokenService.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("Token is invalid!");

            return Ok();
        } 
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
      