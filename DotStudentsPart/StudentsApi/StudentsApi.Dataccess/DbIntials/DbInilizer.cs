using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudentsApi.Dataccess.Data;
using StudentsApi.Model;
using StudentsAsp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Dataccess.DIDbInilizer
{

        public class DbInilizer : IDbInilizer
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly ApplicatioDbContext _db;

            public DbInilizer(
                UserManager<ApplicationUser> userManager,
                RoleManager<IdentityRole> roleManager,
                ApplicatioDbContext db)
            {
                _userManager = userManager;
                _roleManager = roleManager;
                _db = db;
            }

            public void intials()
            {
                try
                {
                    if (_db.Database.GetPendingMigrations().Count() > 0)
                    {
                        _db.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {

                }
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Indi)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@DotnetApplicationMaster.com",
                    Email = "admin@DotnetApplicationMaster.com",
                    Name = "Esraa Aseem",
                    PhoneNumber = "011782399",
                    StreetAdress = "sohage,Garge",
                    PostalCode = "34897",
                    City = "sohag"
                }, "Admin123*").GetAwaiter().GetResult();
                ApplicationUser user = _db.ApplicatioUsers.FirstOrDefault(u => u.Email == "admin@DotnetApplicationMaster.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

            }
            return;
            }
        }


    
}
