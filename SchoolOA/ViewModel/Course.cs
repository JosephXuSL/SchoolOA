using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class CourseRequestBody
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string CourseName { get; set; }
        public string Textbook { get; set; }

    }
}
