using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StudentsApi.Model.ViewModel
{
    public class StudentUpdateRequest
    {
        public Guid id { get; set; }

        [Required]
        public string firstName { get; set; }
        public string? lastName { get; set; }
        [Required]
        public string address { get; set; }
        public long phone { get; set; }
        [Required]
        public string email { get; set; }
        //[ValidateNever]
        public IFormFile? imgFileUrl { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? imgUrl { get; set; }

        public DateTime? birthOfData { get; set; }
        public int genderId { get; set; }
    }
}
