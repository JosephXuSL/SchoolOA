using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolOA.Context;
using SchoolOA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Repositories
{
    public class SchoolRepository
    {
        protected SchoolContext _context;
        private readonly ILogger<SchoolRepository> _logger;
        private readonly IMapper _mapper;

        public SchoolRepository(SchoolContext context, ILogger<SchoolRepository> logger, IMapper mapper) {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        #region Course
        public bool AddCourses(IEnumerable<Course> courses)
        {
            this._context.Courses.AddRange(courses);
            return SaveChanges();
        }

        public bool RemoveCourses(IEnumerable<int> idList)
        {
            var courses = this._context.Courses.Where(x => idList.Contains(x.Id)).ToList();
            this._context.Courses.RemoveRange(courses);
            return SaveChanges();
        }

        public bool UpdateCourses(IEnumerable<Course> courses)
        {

            this._context.Courses.UpdateRange(courses);
            return SaveChanges();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return this._context.Courses.ToList();
        }

        public IEnumerable<Course> GetCourseByIds(IEnumerable<int> idList )
        {
            return this._context.Courses.Where(x=> idList.Contains(x.Id)).ToList();
        }

        public IEnumerable<Course> GetCourseByCourseName(string name)
        {
            return this._context.Courses.Where(x => x.CourseName == name).ToList();
        }
        #endregion Course

        #region Major
        public bool AddMajors(IEnumerable<Major> majors)
        {
            this._context.Majors.AddRange(majors);
            return SaveChanges();
        }

        public bool RemoveMajors(IEnumerable<int> idList)
        {
            var majors = this._context.Majors.Where(x => idList.Contains(x.Id)).ToList();
            this._context.Majors.RemoveRange(majors);
            return SaveChanges();
        }

        public bool UpdateMajors(IEnumerable<Major> majors)
        {

            this._context.Majors.UpdateRange(majors);
            return SaveChanges();
        }

        public IEnumerable<Major> GetAllMajors()
        {
            return this._context.Majors.ToList();
        }

        public IEnumerable<string> GetDepartmentByGrade(string grade)
        {
            var departmentList = new List<string>();
            this._context.Majors.Where(m=>m.Grade== grade).ToList().ForEach(m=> { if (!departmentList.Contains(m.Department)) { departmentList.Add(m.Department); } });
            return departmentList;
        }

        public IEnumerable<string> GetMajorNameByGradeAndDepartment(string grade, string department)
        {
            var MajorName = new List<string>();
            this._context.Majors.Where(m => (grade== null? true : m.Grade== grade) && (department == null ? true : m.Department== department))
                .ToList().ForEach(m => { if (!MajorName.Contains(m.MajorName)) { MajorName.Add(m.MajorName); } });
            return MajorName;
        }

        public IEnumerable<Major> GetMajors(GetMajorRequestBody request)
        {            
            return this._context.Majors.Where(m => (request.Grade == null ? true : m.Grade == request.Grade)
            && (request.Department == null ? true : m.Department == request.Department) 
            && (request.MajorName == null ? true : m.MajorName ==request.MajorName)).ToList();
        }

        public IEnumerable<Major> GetMajorByIds(IEnumerable<int> idList)
        {
            return this._context.Majors.Where(x => idList.Contains(x.Id)).ToList();
        }
        #endregion Major

        #region Teacher
        public bool AddTeachers(IEnumerable<Teacher> teachers)
        {
            this._context.Teachers.AddRange(teachers);
            return SaveChanges();
        }

        public bool RemoveTeachers(IEnumerable<int> idList)
        {
            var teachers = this._context.Teachers.Where(x => idList.Contains(x.Id)).ToList();
            this._context.Teachers.RemoveRange(teachers);
            return SaveChanges();
        }

        public bool UpdateTeachers(IEnumerable<Teacher> teachers)
        {

            this._context.Teachers.UpdateRange(teachers);
            return SaveChanges();
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return this._context.Teachers.ToList();
        }

        public IEnumerable<Teacher> GetTeachersByName(string name)
        {
            return this._context.Teachers.Where(t=>t.Name == name).ToList();
        }

        public IEnumerable<Teacher> GetTeacherByIds(IEnumerable<int> idList)
        {
            return this._context.Teachers.Where(x => idList.Contains(x.Id)).ToList();
        }
        #endregion Teacher

        #region TeacherReceivedAward
        public bool AddTeacherReceivedAward(IEnumerable<TeacherReceivedAward> awards)
        {
            this._context.TeacherReceivedAward.AddRange(awards);
            return SaveChanges();
        }

        public bool RemoveTeacherReceivedAward(IEnumerable<int> idList)
        {
            var awards = this._context.TeacherReceivedAward.Where(x => idList.Contains(x.Id)).ToList();
            this._context.TeacherReceivedAward.RemoveRange(awards);
            return SaveChanges();
        }

        public bool UpdateTeacherReceivedAward(IEnumerable<TeacherReceivedAward> awards)
        {
            this._context.TeacherReceivedAward.UpdateRange(awards);
            return SaveChanges();
        }

        public IEnumerable<TeacherReceivedAward> GetAllTeacherReceivedAward()
        {
            return this._context.TeacherReceivedAward.Include(t=>t.Teacher).ToList();
        }

        public IEnumerable<TeacherReceivedAward> GetTeacherReceivedAwardByTeacherId(int id)
        {
            return this._context.TeacherReceivedAward.Where(x => x.TeacherId == id).ToList();
        }
        #endregion TeacherReceivedAward

        #region TeacherAccount
        public bool AddTeacherAccounts(IEnumerable<TeacherAccount> accounts)
        {
            this._context.TeacherAccounts.AddRange(accounts);
            foreach (var account in accounts)
            {
                if (account.Teacher != null)
                {
                    this._context.Entry(account.Teacher).State = EntityState.Unchanged;
                }
            }
            return SaveChanges();
        }

        public bool RemoveTeacherAccounts(IEnumerable<int> idList)
        {
            var accounts = this._context.TeacherAccounts.Where(x => idList.Contains(x.Id)).ToList();
            this._context.TeacherAccounts.RemoveRange(accounts);
            return SaveChanges();
        }

        public bool UpdateTeacherAccounts(IEnumerable<TeacherAccount> accounts)
        {
            this._context.TeacherAccounts.UpdateRange(accounts);
            foreach (var account in accounts)
            {
                if (account.Teacher != null)
                {
                    this._context.Entry(account.Teacher).State = EntityState.Detached;
                }
            }
            return SaveChanges();
        }

        public IEnumerable<TeacherAccount> GetAllTeacherAccounts()
        {
            return this._context.TeacherAccounts.Include(t => t.Teacher).ToList();
        }

        public TeacherAccount GetTeacherAccountByTeacherId(int id)
        {
            var result= this._context.TeacherAccounts.Where(x => x.TeacherId == id).FirstOrDefault();
            if (result!=null)
            {
                result.Teacher= this._context.Teachers.Where(x => x.TeacherNumber == result.AccountName).FirstOrDefault();
            }
            return result;
        }

        public TeacherAccount GetTeacherAccountByName(string accountName)
        {
            var result = this._context.TeacherAccounts.Where(x => x.AccountName == accountName).FirstOrDefault();
            if (result != null)
            {
                result.Teacher = this._context.Teachers.Where(x => x.TeacherNumber == result.AccountName).FirstOrDefault();
            }
            return result;

        }
        public TeacherAccount GetTeacherAccountByTeacherNumber(string teacherNumber)
        {
            var result = this._context.TeacherAccounts.Where(x => x.Teacher.TeacherNumber == teacherNumber).FirstOrDefault();
            if (result != null)
            {
                result.Teacher = this._context.Teachers.Where(x => x.TeacherNumber == result.AccountName).FirstOrDefault();
            }
            return result;
        }

        public bool UpdateTeacherAccountPassWord(TeacherAccount account)
        {
            this._context.TeacherAccounts.Update(account);
            return SaveChanges();
        }

        public TeacherAccount GetTeacherAccountByTeacherNum(string Num)
        {
            var result = this._context.TeacherAccounts.Where(x => x.Teacher.TeacherNumber == Num).FirstOrDefault();
            if (result != null)
            {
                result.Teacher = this._context.Teachers.Where(x => x.TeacherNumber == Num).FirstOrDefault();
            }
            return result;
        }
        #endregion TeacherAccount

        #region Class
        public bool AddClasses(IEnumerable<Class> classes)
        {
            this._context.Classes.AddRange(classes);
            return SaveChanges();
        }

        public bool RemoveClasses(IEnumerable<int> idList)
        {
            var classes = this._context.Classes.Where(x => idList.Contains(x.Id)).ToList();
            this._context.Classes.RemoveRange(classes);
            return SaveChanges();
        }

        public bool UpdateClasses(IEnumerable<Class> classes)
        {
            this._context.Classes.UpdateRange(classes);
            return SaveChanges();
        }

        public IEnumerable<Class> GetAllClasses()
        {
            return this._context.Classes.Include(t => t.Mentor).Include(t => t.Major).ToList();
        }

        public IEnumerable<Class> GetClassesByIds(IEnumerable<int> idList)
        {
            return this._context.Classes.Where(x => idList.Contains(x.Id)).Include(x=>x.Major).Include(x => x.Mentor).ToList();
        }

        public IEnumerable<Class> GetClassesByMajorIds(IEnumerable<int> idList)
        {
            return this._context.Classes.Where(x => idList.Contains(x.MajorId)).Include(x => x.Major).Include(x => x.Mentor).ToList();
        }

        public IEnumerable<Class> GetClassesByMentorId(int id)
        {
            return this._context.Classes.Where(x => x.TeacherId== id).Include(x => x.Major).Include(x => x.Mentor).ToList();
        }

        public IEnumerable<Class> GetClassesByTeacheNumber(string number)
        {
            return this._context.Classes.Where(x => x.Mentor.TeacherNumber == number).Include(x => x.Major).Include(x => x.Mentor).ToList();
        }

        public IEnumerable<Class> GetClassesByClassInfo(ClassInfoRequestBody p)
        {
            return this._context.Classes.Where(x => (p.Department==null? true: p.Department==x.Major.Department) 
            && (p.Grade == null ? true : p.Grade == x.Major.Grade)
            && (p.MajorName == null ? true : p.MajorName == x.Major.MajorName)
            && (p.ClassNumber == null ? true : p.ClassNumber == x.ClassNumber)).Include(c => c.Major).Include(c => c.Mentor).ToList();
        }
        #endregion Class

        #region Student
        public bool AddStudents(IEnumerable<Student> students)
        {
            this._context.Students.AddRange(students);
            return SaveChanges();
        }

        public bool RemoveStudents(IEnumerable<int> idList)
        {
            var students = this._context.Students.Where(x => idList.Contains(x.Id)).ToList();
            this._context.Students.RemoveRange(students);
            return SaveChanges();
        }

        public bool UpdateStudents(IEnumerable<Student> students)
        {
            this._context.Students.UpdateRange(students);
            return SaveChanges();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return this._context.Students.Include(t => t.Class).ThenInclude(c=>c.Mentor).Include(t => t.Major).ToList();
        }

        public IEnumerable<Student> GetStudentsByName(string name)
        {
            return this._context.Students.Where(s=>s.Name== name).Include(t => t.Class).ThenInclude(c => c.Mentor).Include(t => t.Major).ToList();
        }

        public Student GetStudentByIDCardNumber(string id)
        {
            return this._context.Students.Where(s => s.IdentityCardNumber == id).Include(t => t.Class).ThenInclude(c => c.Mentor).Include(t => t.Major).FirstOrDefault();
        }

        public IEnumerable<Student> GetStudentsByClassInfo(ClassInfoRequestBody request)
        {
            var classes = GetClassesByClassInfo(request);
            
            return this._context.Students.Where(s => classes.Contains(s.Class)).Include(t => t.Class).ThenInclude(c => c.Mentor).Include(t => t.Major).ToList();
        }

        public IEnumerable<Student> GetStudentsByClassId(int id)
        {
            return this._context.Students.Where(s => s.ClassId == id).Include(t => t.Class).ThenInclude(c => c.Mentor).Include(t => t.Major).ToList();
        }

        public IEnumerable<Student> GetStudentsByIds(IEnumerable<int> idList)
        {
            return this._context.Students.Where(s => idList.Contains(s.Id)).Include(t => t.Class).ThenInclude(c => c.Mentor).Include(t => t.Major).ToList();
        }

        public Student GetStudentByStudentNumber(string data)
        {
            return this._context.Students.Where(s => s.StudentNumber == data).FirstOrDefault();
        }
        #endregion Student

        #region Enroll
        public bool AddEnroll(IEnumerable<Enroll> students)
        {
            this._context.Enroll.AddRange(students);
            return SaveChanges();
        }

        public bool RemoveEnroll(IEnumerable<int> idList)
        {
            var students = this._context.Enroll.Where(x => idList.Contains(x.Id)).ToList();
            this._context.Enroll.RemoveRange(students);
            return SaveChanges();
        }

        public bool UpdateEnroll(IEnumerable<Enroll> students)
        {
            this._context.Enroll.UpdateRange(students);
            return SaveChanges();
        }

        public IEnumerable<Enroll> GetAllEnroll()
        {
            return this._context.Enroll.Include(t => t.Major).ToList();
        }
        public IEnumerable<Enroll> GetEnrollByMajor(GetMajorRequestBody request)
        {
            var majors = GetMajors(request);
            return this._context.Enroll.Include(t => t.Major).Where(e=> majors.Contains(e.Major)).ToList();
        }

        public Enroll GetEnrollByIDCardNumber(string id)
        {
            return this._context.Enroll.Where(e => e.IdentityCardNumber == id).Include(t => t.Major).FirstOrDefault();
        }

        #endregion Enroll

        #region CourseResponsibleByTeacher
        public bool AddCourseResponsibleByTeacher(IEnumerable<CourseResponsibleByTeacher> information)
        {
            this._context.CourseResponsibleByTeacher.AddRange(information);
            return SaveChanges();
        }

        public bool RemoveCourseResponsibleByTeacher(IEnumerable<int> idList)
        {
            var information = this._context.CourseResponsibleByTeacher.Where(x => idList.Contains(x.Id)).ToList();
            this._context.CourseResponsibleByTeacher.RemoveRange(information);
            return SaveChanges();
        }

        public bool UpdateCourseResponsibleByTeacher(IEnumerable<CourseResponsibleByTeacher> information)
        {
            this._context.CourseResponsibleByTeacher.UpdateRange(information);
            return SaveChanges();
        }

        public IEnumerable<CourseResponsibleByTeacher> GetAllCourseResponsibleByTeacher()
        {
            return this._context.CourseResponsibleByTeacher
                .Include(i => i.Class)
                .Include(i => i.Course)
                .Include(i => i.Teacher).ToList();
        }

        public IEnumerable<CourseResponsibleByTeacher> GetCourseAndClassByTeacherId( int id)
        {
            return this._context.CourseResponsibleByTeacher.Where(c=>c.TeacherId==id)
                .Include(i => i.Class)
                .Include(i => i.Course)
                .Include(i => i.Teacher).ToList();
        }
        #endregion CourseResponsibleByTeacher

        #region CourseSchedule
        public bool AddCourseSchedule(IEnumerable<CourseSchedule> information)
        {
            this._context.CourseSchedule.AddRange(information);
            return SaveChanges();
        }

        public bool RemoveCourseSchedule(IEnumerable<int> idList)
        {
            var information = this._context.CourseSchedule.Where(x => idList.Contains(x.Id)).ToList();
            this._context.CourseSchedule.RemoveRange(information);
            return SaveChanges();
        }

        public bool UpdateCourseSchedule(IEnumerable<CourseSchedule> information)
        {
            this._context.CourseSchedule.UpdateRange(information);
            return SaveChanges();
        }

        public IEnumerable<CourseSchedule> GetAllCourseSchedule()
        {
            return this._context.CourseSchedule
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i=>i.Teacher)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Course)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Class)
                .ToList();
        }

        public IEnumerable<CourseSchedule> GetCourseScheduleByTeacherId(int id)
        {
            return this._context.CourseSchedule
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Teacher)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Course)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Class)
                .Where(c=> c.TeacherCourseInfo.TeacherId == id)
                .ToList();
        }

        public IEnumerable<CourseSchedule> GetCourseScheduleByClassId(int id)
        {
            return this._context.CourseSchedule
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Teacher)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Course)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Class)
                .Where(c => c.TeacherCourseInfo.ClassId == id)
                .ToList();
        }
        #endregion CourseSchedule

        #region CourseSelection
        public bool AddCourseSelection(IEnumerable<CourseSelection> information)
        {
            this._context.CourseSelection.AddRange(information);
            return SaveChanges();
        }

        public bool RemoveCourseSelection(IEnumerable<int> idList)
        {
            var information = this._context.CourseSelection.Where(x => idList.Contains(x.Id)).ToList();
            this._context.CourseSelection.RemoveRange(information);
            return SaveChanges();
        }

        public bool UpdateCourseSelection(IEnumerable<CourseSelection> information)
        {
            this._context.CourseSelection.UpdateRange(information);
            return SaveChanges();
        }

        public IEnumerable<CourseSelection> GetAllCourseSelection()
        {
            return this._context.CourseSelection
                .Include(i=>i.Student)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Teacher)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Course)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Class)
                .ToList();
        }

        public IEnumerable<CourseSelection> GetCourseSelectionByStudentId(int id)
        {
            return this._context.CourseSelection
                .Where (i=>i.StudentId==id)
                .Include(i => i.Student)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Teacher)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Course)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Class)
                .ToList();
        }

        public IEnumerable<CourseSelection> GetCourseSelectionByTeacher(string teacherNumber)
        {
            var teacherid = GetTeacherAccountByTeacherNumber(teacherNumber).TeacherId;
            return GetAllCourseSelection().Where(t => t.TeacherCourseInfo.TeacherId == teacherid);
        }
        #endregion CourseSelection

        #region Examination
        public bool AddExaminations(IEnumerable<Examination> exams)
        {
            this._context.Examinations.AddRange(exams);
            return SaveChanges();
        }

        public IEnumerable<ExaminationImportBody> ImportExaminations(IEnumerable<ExaminationImportBody> exams)
        {
            var importFailedList = new List<ExaminationImportBody>();
            foreach (var exam in exams) {
                if (exam.StudentNumber.Length > 0)
                {
                    var student = GetStudentByStudentNumber(exam.StudentNumber);
                    if (student != null) {
                        var e = _mapper.Map<Examination>(exam);
                        e.StudentId = student.Id;
                        e.Student = student;
                        this._context.Examinations.Add(e);
                    }
                    else
                    {
                        importFailedList.Add(exam);
                    }

                }
                else {
                    importFailedList.Add(exam);
                }            
            }
            if (SaveChanges()) {
                return importFailedList;
            }
            else
            {
                return exams;
            }            
        }

        public bool RemoveExaminations(IEnumerable<int> idList)
        {
            var exams = this._context.Examinations.Where(x => idList.Contains(x.Id)).ToList();
            this._context.Examinations.RemoveRange(exams);
            return SaveChanges();
        }

        public bool UpdateExaminations(IEnumerable<Examination> exams)
        {
            this._context.Examinations.UpdateRange(exams);
            return SaveChanges();
        }

        public IEnumerable<Examination> GetAllExaminations()
        {
            return this._context.Examinations
                .Include(i => i.Student)
                .Include(i => i.Major)
                .Include(i => i.Course)
                .ToList();
        }

        public IEnumerable<Examination> GetExaminationsByStudentsId(IEnumerable<int> idList)
        {
            return this._context.Examinations
                .Where(i=> idList.Contains(i.StudentId))
                .Include(i => i.Student)
                .Include(i => i.Major)
                .Include(i => i.Course)
                .ToList();
        }
        #endregion Examination

        public TeacherAccount GetTeacherAccountByTeacherNameAndPassWord(string loginName, string password)
        {
            var result = this._context.TeacherAccounts.Where(x => x.AccountName == loginName && x.Password == password).FirstOrDefault();
            if (result != null)
            {
                result.Teacher = this._context.Teachers.Where(x => x.TeacherNumber == loginName).FirstOrDefault();
            }
            return result;
        }

        private bool SaveChanges()
        {
            return this._context.SaveChanges() > 0;
        }
    }
}
