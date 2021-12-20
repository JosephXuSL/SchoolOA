using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Entities
{
    public class GetMajorRequestBody
    {       
        public string Department { get; set; }
        public string Grade { get; set; }
        public string MajorName { get; set; }
    }
}
