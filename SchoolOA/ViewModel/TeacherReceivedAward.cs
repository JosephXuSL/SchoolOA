using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.ViewModel
{
    public class TeacherReceivedAwardRequestBody
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string AwardName { get; set; }
        [Required]
        public DateTime AwardDate { get; set; }
        public string Detail { get; set; }
        [Required]
        public int TeacherId { get; set; }
    }
}
