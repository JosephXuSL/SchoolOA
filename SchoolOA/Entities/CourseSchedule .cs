using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class CourseSchedule
    {
        public int Id { get; set; }
        public int TeacherCourseInfoId { get; set; }
        public CourseResponsibleByTeacher TeacherCourseInfo { get; set; }        
        public string ScheduledWeekday { get; set; }
        public string ScheduledTime { get; set; }

    }
}
