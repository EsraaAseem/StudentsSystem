using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using StudentsApi.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentsApi.Model.ViewModel
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
