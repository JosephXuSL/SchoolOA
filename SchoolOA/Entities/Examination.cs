using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class Examination
    {
        public int Id { get; set; }
        public string Semester { get; set; }
        public int MajorId { get; set; }
        public Major Major { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        //public string ExamSubject { get; set; }
        //public DateTime ExamDate { get; set; }
        //public int? CourseResponsibleByTeacherId { get; set; }
        //public CourseResponsibleByTeacher TeacherCourseInfo { get; set; }
        //public string Perormance { get; set; }
        public float Score { get; set; }
    }
}
