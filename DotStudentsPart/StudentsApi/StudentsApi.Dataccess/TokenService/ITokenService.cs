using StudentsApi.Model;
using StudentsApi.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Dataccess.TokenService
{
    public interface ITokenService
    {
        // string CreateToken(ApplicationUser user);
        Task<userDto> LoginAsync(loginUser Input);
        Task<userDto> RegisterAsync(registerUser Input);
        Task<string> GetCurentUser();
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<userDto> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);

    }
}
