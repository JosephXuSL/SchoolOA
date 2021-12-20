using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class ClassRequestBody
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ClassNumber { get; set; }
        [Required]
        public int MajorId { get; set; }
        [Required]
        public int TeacherId { get; set; }
    }

    public class ClassInfoRequestBody
    {
        public string ClassNumber { get; set; }        
        public string Department { get; set; }
        public string Grade { get; set; }
        public string MajorName { get; set; }
    }
}
