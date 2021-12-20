using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class CourseSelection
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseResponsibleByTeacherId { get; set; }
        public CourseResponsibleByTeacher TeacherCourseInfo { get; set; }       
    }
}
