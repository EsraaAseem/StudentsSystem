using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Model
{
    public class Students
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string firstName { get; set; }
        public string? lastName { get; set; }
        [Required]
        public string address { get; set; }
        public long phone { get; set; }
        [Required]
        public string email { get; set; }
        [ValidateNever]
        public string? imgUrl { get; set; }
        public DateTime? birthOfData { get; set; }
        public int genderId { get; set; }
        [ForeignKey("genderId")]
        [ValidateNever]
        public Gender gender { get; set; }
    }
}
