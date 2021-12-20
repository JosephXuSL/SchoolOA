using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class TeacherDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TeacherNumber { get; set; }
        public string TeacherStatus { get; set; }
        public string TeacherComment { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMentor { get; set; }
    }
}
