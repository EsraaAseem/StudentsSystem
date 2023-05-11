using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using StudentsApi.Model.ViewModel;

namespace StudentsApi.Model
{
        public class ApplicationUser : IdentityUser
        {
            [Required]
            public string Name { get; set; }
            public string? City { get; set; }
            public string? StreetAdress { get; set; }
            public string? PostalCode { get; set; }
            public string? State { get; set; }
          public List<RefreshToken>? RefreshTokens { get; set; }


        }
}

