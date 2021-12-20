using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class StudentRequestBody
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Sex { get; set; }
        [Required]
        public string IdentityCardNumber { get; set; }
        [Required]
        public string StudentNumber { get; set; }
        public string StudentStatus { get; set; }
        [Required]
        public string HomeAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Portrait { get; set; }
        [Required]
        public int MajorId { get; set; }
        public string Apartment { get; set; }
        public string Chamber { get; set; }
        public string Bed { get; set; }
        [Required]
        public int ClassId { get; set; }
    }
}
