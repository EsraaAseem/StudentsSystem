using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StudentsApi.Model.ViewModel
{
    public class registerUser
    {

        /*  [Required]
          [EmailAddress]
          [Display(Name = "Email")]*/
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        public string? City { get; set; }
        [Required]
        public string StreetAdress { get; set; }
        public string? PostalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? State { get; set; }
         public string? Role { get; set; }
        [Required]
        public string UserName { get; set; }

    }
}
