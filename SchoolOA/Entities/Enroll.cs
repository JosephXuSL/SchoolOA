using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class Enroll
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string IdentityCardNumber { get; set; }
        public string HomeAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Portrait { get; set; }
        public int MajorId { get; set; }
        public Major Major { get; set; }
        public string RequestResult { get; set; }
    }
}
