using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class ExaminationRequestBody
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string ExamSubject { get; set; }
        [Required]
        public DateTime ExamDate { get; set; }
        public int? CourseResponsibleByTeacherId { get; set; }
        public string Perormance { get; set; }
        public float? Score { get; set; }
    }
}
