using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class TeacherReceivedAward
    {
        public int Id { get; set; }
        public string AwardName { get; set; }
        public DateTime AwardDate { get; set; }
        public string Detail { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}
