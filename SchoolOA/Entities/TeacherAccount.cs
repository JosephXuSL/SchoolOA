using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class TeacherAccount
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string AccountStatus { get; set; }
        public bool IsAdminAccount { get; set; }
    }
}
