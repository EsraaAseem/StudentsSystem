using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Dataccess.Data
{
    public class ApplicatioDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicatioDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Students> students { get; set; }
        public DbSet<Gender>gender { get; set; }
        public DbSet<ApplicationUser> ApplicatioUsers { get; set; }

    }
    
}
