using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string IdentityCardNumber { get; set; }
        public string StudentNumber { get; set; }
        public string StudentStatus { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Portrait { get; set; }
        public int MajorId { get; set; }
        public Major Major { get; set; }
        public string Apartment { get; set; }
        public string Chamber { get; set; }
        public string Bed { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
    }
}
