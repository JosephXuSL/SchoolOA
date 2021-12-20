using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class Class
    {
        public int Id { get; set; }
        public string ClassNumber { get; set; }
        public int MajorId { get; set; }
        public Major Major { get; set; }
        public int TeacherId { get; set; }
        public Teacher Mentor { get; set; }
    }
}
