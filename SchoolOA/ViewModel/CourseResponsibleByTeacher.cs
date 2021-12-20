using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class CourseResponsibleByTeacherRequestBody
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public int? ClassId { get; set; }
        [Required]
        public string Semester { get; set; }
    }
}
