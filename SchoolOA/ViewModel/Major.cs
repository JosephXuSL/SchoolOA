using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class MajorRequestBody
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Grade { get; set; }
        [Required]
        public string MajorName { get; set; }
    }
    public class GetMajorRequestBody
    {       
        public string Department { get; set; }
        public string Grade { get; set; }
        public string MajorName { get; set; }
    }
}
