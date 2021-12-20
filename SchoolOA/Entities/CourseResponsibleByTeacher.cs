using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class CourseResponsibleByTeacher
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }        
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int? ClassId { get; set; }
        public Class Class { get; set; }
        public string Semester { get; set; }
    }
}
