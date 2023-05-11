using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Model.ViewModel
{
    public class appId
    {

        [Key]
        public Guid Id { get; set; }
        public Guid ?ProductId { get; set; }
        public string ApplicationUserId { get; set; }
        [Range(1, 1000, ErrorMessage = "out of range")]
        public int? Count { get; set; }
        public double? Price { get; set; }
    }
}
