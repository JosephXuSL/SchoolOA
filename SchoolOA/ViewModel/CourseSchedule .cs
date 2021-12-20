using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class CourseScheduleRequestBody
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CourseResponsibleByTeacherId { get; set; }
        [Required]
        public string ScheduledWeekday { get; set; }
        [Required]
        public string ScheduledTime { get; set; }

    }

    public class GetCourseScheduleRequestBody
    {   
        public int TeacherId { get; set; }        
        public int ClassId { get; set; }        
        public int StudentId { get; set; }

    }
}
