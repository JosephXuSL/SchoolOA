using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeacherNumber { get; set; }
        public string TeacherStatus { get; set; }
        public string TeacherComment { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsMentor { get; set; }
    }
}
