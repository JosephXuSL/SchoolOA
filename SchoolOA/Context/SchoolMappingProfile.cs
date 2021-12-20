using AutoMapper;
using SchoolOA.Entities;
using SchoolOA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Context
{
    public class SchoolMappingProfile: Profile
    {
        public SchoolMappingProfile()
        {
            CreateMap<TeacherReceivedAwardRequestBody, TeacherReceivedAward>().ReverseMap();
            //.ForMember(x=>x.Id, ex=>ex.MapFrom(x=>x.Id));
            CreateMap<TeacherAccountRequestBody, TeacherAccount>().ReverseMap();
            CreateMap<ClassRequestBody, Class>().ReverseMap();
            CreateMap<StudentRequestBody, Student>().ReverseMap();
            CreateMap<EnrollRequestBody, Enroll>().ReverseMap();
            CreateMap<CourseResponsibleByTeacherRequestBody, CourseResponsibleByTeacher>().ReverseMap();
            CreateMap<CourseScheduleRequestBody, CourseSchedule>().ReverseMap();
            CreateMap<CourseSelectionRequestBody, CourseSelection>().ReverseMap();
            CreateMap<ExaminationRequestBody, Examination>().ReverseMap();
        }
    }
}
