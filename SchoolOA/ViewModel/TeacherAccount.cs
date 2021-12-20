﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class TeacherAccountRequestBody
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int TeacherId { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string Password { get; set; }
        public string AccountStatus { get; set; }
        [Required]
        public bool IsMentorAccount { get; set; }
    }
}
