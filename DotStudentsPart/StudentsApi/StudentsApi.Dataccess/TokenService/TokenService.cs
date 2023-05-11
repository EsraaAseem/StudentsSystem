using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Microsoft.IdentityModel.Tokens;
using StudentsApi.Model;
using StudentsApi.Model.ViewModel;
using StudentsAsp.Utility;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Dataccess.TokenService
{
    public class TokenService:ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JWT _jwt;
        public TokenService(UserManager<ApplicationUser> userManager,
             RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        private async Task<List<Claim>> getclaimroles(ApplicationUser user)
        {
            var _option = new List<Claim>();
            var claims = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id)
            };
            var userclaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userclaims);
            var roleUser = await _userManager.GetRolesAsync(user);
            foreach (var userrole in roleUser)
            {
                var role = await _userManager.FindByNameAsync(userrole);
                if (role != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userrole));
                    var roleClaims = await _userManager.GetClaimsAsync(role);
                    foreach (var claim in roleClaims)
                    {
                        claims.Add(claim);
                    }
                }
            }
            return claims;
        }
       
        private async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {


            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));
            var claimsrole =getclaimroles(user);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;

           
        }

        public async Task<userDto> LoginAsync(loginUser Input)
        {
       
            var user = await _userManager.FindByEmailAsync(Input.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, Input.Password))
            {
                return new userDto { message = "there is not email or password" };
            }
            var userdto = new userDto();

            var jwtSecurityToken = await CreateToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            userdto.Email = user.Email;
            userdto.UserName = user.UserName;
            userdto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            userdto.isAuthenticated = true;
            userdto.ExpiresOn = jwtSecurityToken.ValidTo;
            userdto.Role = roles[0];
            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                userdto.RefreshToken = activeRefreshToken.Token;
                userdto.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                userdto.RefreshToken = refreshToken.Token;
                userdto.RefreshTokenExpiration = refreshToken.ExpiresOn;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }
            return userdto;
        }

        public async Task<userDto> RegisterAsync(registerUser Input)
        {
            // await _emailStore.SetEmailAsync(user, Input.Email);
            //  await _userStore.SetUserNameAsync(user, Input.Name);
            if (await _userManager.FindByEmailAsync(Input.Email) is not null)
            {
                return new userDto { message = "this email is already created" };
            }
            if (await _userManager.FindByNameAsync(Input.UserName) is not null)
            {
                return new userDto { message = "this user name is already created" };
            }
            var user = new ApplicationUser
            {
                Email = Input.Email,
                Name = Input.Name,
                City = Input.City,
                StreetAdress = Input.StreetAdress,
                PostalCode = Input.PostalCode,
                PhoneNumber = Input.PhoneNumber,
                State = Input.State,
                UserName = Input.UserName
            };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                var role = SD.Role_User_Indi;
                if (Input.Role == null)
                {
                    await _userManager.AddToRoleAsync(user, SD.Role_User_Indi);
                }
                else
                {
                    await _userManager.AddToRoleAsync(user, Input.Role);
                    role = Input.Role;
                }
                var jwtSecurityToken = await CreateToken(user);
                var refreshToken = GenerateRefreshToken();
                user.RefreshTokens?.Add(refreshToken);
                await _userManager.UpdateAsync(user);


                return new userDto
                {
                    Email = user.Email,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    isAuthenticated = true,
                    Role = role,
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    UserName = user.UserName,
                    RefreshToken = refreshToken.Token,
                    RefreshTokenExpiration = refreshToken.ExpiresOn
                };
            }
            else
            {
                Console.WriteLine("error");
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description},";
                return new userDto { message = errors };
            }
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Sonething went wrong";
        }


        public async Task<userDto> RefreshTokenAsync(string token)
        {
            var userdto = new userDto();

            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
            {
                userdto.message = "Invalid token";
                return userdto;
            }

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                userdto.message = "Inactive token";
                return userdto;
            }

            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtSecurityToken = await CreateToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            userdto.Email = user.Email;
            userdto.UserName = user.UserName;
            userdto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            userdto.isAuthenticated = true;
            userdto.ExpiresOn = jwtSecurityToken.ValidTo;
            userdto.Role = roles[0];
            userdto.RefreshToken = newRefreshToken.Token;
            userdto.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
            return userdto;
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return true;
        }
        
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }

        public async Task<string> GetCurentUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.Identity.Name;
           
            return userId;
        }

    }
}
