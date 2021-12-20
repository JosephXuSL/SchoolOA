using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class EnrollRequestBody
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
        public string HomeAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Portrait { get; set; }
        [Required]
        public int MajorId { get; set; }
        public string RequestResult { get; set; }
    }
}
