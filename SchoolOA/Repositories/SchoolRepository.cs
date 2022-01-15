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
            _context.Courses.AddRange(courses);
            return SaveChanges();
        }

        public bool RemoveCourses(IEnumerable<int> idList)
        {
            var courses = _context.Courses.Where(x => idList.Contains(x.Id)).ToList();
            _context.Courses.RemoveRange(courses);
            return SaveChanges();
        }

        public bool UpdateCourses(IEnumerable<Course> courses)
        {

            _context.Courses.UpdateRange(courses);
            return SaveChanges();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public IEnumerable<Course> GetCourseByIds(IEnumerable<int> idList )
        {
            return _context.Courses.Where(x=> idList.Contains(x.Id)).ToList();
        }

        public IEnumerable<Course> GetCourseByCourseName(string name)
        {
            return _context.Courses.Where(x => x.CourseName == name).ToList();
        }
        #endregion Course

        #region Major
        public bool AddMajors(IEnumerable<Major> majors)
        {
            _context.Majors.AddRange(majors);
            return SaveChanges();
        }

        public bool RemoveMajors(IEnumerable<int> idList)
        {
            var majors = _context.Majors.Where(x => idList.Contains(x.Id)).ToList();
            _context.Majors.RemoveRange(majors);
            return SaveChanges();
        }

        public bool UpdateMajors(IEnumerable<Major> majors)
        {

            _context.Majors.UpdateRange(majors);
            return SaveChanges();
        }

        public IEnumerable<Major> GetAllMajors()
        {
            return _context.Majors.ToList();
        }

        public IEnumerable<string> GetDepartmentByGrade(string grade)
        {
            var departmentList = new List<string>();
            _context.Majors.Where(m=>m.Grade== grade).ToList().ForEach(m=> { if (!departmentList.Contains(m.Department)) { departmentList.Add(m.Department); } });
            return departmentList;
        }

        public IEnumerable<string> GetMajorNameByGradeAndDepartment(string grade, string department)
        {
            var MajorName = new List<string>();
            _context.Majors.Where(m => (grade== null? true : m.Grade== grade) && (department == null ? true : m.Department== department))
                .ToList().ForEach(m => { if (!MajorName.Contains(m.MajorName)) { MajorName.Add(m.MajorName); } });
            return MajorName;
        }

        public IEnumerable<Major> GetMajors(GetMajorRequestBody request)
        {            
            return _context.Majors.Where(m => (request.Grade == null ? true : m.Grade == request.Grade)
            && (request.Department == null ? true : m.Department == request.Department) 
            && (request.MajorName == null ? true : m.MajorName ==request.MajorName)).ToList();
        }

        public IEnumerable<Major> GetMajorByIds(IEnumerable<int> idList)
        {
            return _context.Majors.Where(x => idList.Contains(x.Id)).ToList();
        }
        #endregion Major

        #region Teacher
        public bool AddTeachers(IEnumerable<Teacher> teachers)
        {
            _context.Teachers.AddRange(teachers);
            return SaveChanges();
        }

        public bool RemoveTeachers(IEnumerable<int> idList)
        {
            var teachers = _context.Teachers.Where(x => idList.Contains(x.Id)).ToList();
            _context.Teachers.RemoveRange(teachers);
            return SaveChanges();
        }

        public bool UpdateTeachers(IEnumerable<Teacher> teachers)
        {

            _context.Teachers.UpdateRange(teachers);
            return SaveChanges();
        }

        public IEnumerable<Teacher> GetAllTeachers()
        {
            return _context.Teachers.ToList();
        }

        public IEnumerable<Teacher> GetAllNoAccountTeachers()
        {
            var idList = _context.TeacherAccounts.Select(t=>t.TeacherId);
            return _context.Teachers.Where(t=> !idList.Contains(t.Id)).ToList();
        }

        public IEnumerable<Teacher> GetTeachersByName(string name)
        {
            return _context.Teachers.Where(t=>t.Name == name).ToList();
        }

        public IEnumerable<Teacher> GetTeacherByIds(IEnumerable<int> idList)
        {
            return _context.Teachers.Where(x => idList.Contains(x.Id)).ToList();
        }

        public bool CheckTeacherExistByteacherNumber(string teacherNum)
        {
            return _context.Teachers.Where(x => x.TeacherNumber == teacherNum).Count() > 0;
        }
        #endregion Teacher

        #region TeacherReceivedAward
        public bool AddTeacherReceivedAward(IEnumerable<TeacherReceivedAward> awards)
        {
            _context.TeacherReceivedAward.AddRange(awards);
            return SaveChanges();
        }

        public bool RemoveTeacherReceivedAward(IEnumerable<int> idList)
        {
            var awards = _context.TeacherReceivedAward.Where(x => idList.Contains(x.Id)).ToList();
            _context.TeacherReceivedAward.RemoveRange(awards);
            return SaveChanges();
        }

        public bool UpdateTeacherReceivedAward(IEnumerable<TeacherReceivedAward> awards)
        {
            _context.TeacherReceivedAward.UpdateRange(awards);
            return SaveChanges();
        }

        public IEnumerable<TeacherReceivedAward> GetAllTeacherReceivedAward()
        {
            return _context.TeacherReceivedAward.Include(t=>t.Teacher).ToList();
        }

        public IEnumerable<TeacherReceivedAward> GetTeacherReceivedAwardByTeacherId(int id)
        {
            return _context.TeacherReceivedAward.Where(x => x.TeacherId == id).ToList();
        }
        #endregion TeacherReceivedAward

        #region TeacherAccount
        public bool AddTeacherAccounts(IEnumerable<TeacherAccount> accounts)
        {
            _context.TeacherAccounts.AddRange(accounts);
            foreach (var account in accounts)
            {
                if (account.Teacher != null)
                {
                    _context.Entry(account.Teacher).State = EntityState.Unchanged;
                }
            }
            return SaveChanges();
        }

        public bool RemoveTeacherAccounts(IEnumerable<int> idList)
        {
            var accounts = _context.TeacherAccounts.Where(x => idList.Contains(x.Id)).ToList();
            _context.TeacherAccounts.RemoveRange(accounts);
            return SaveChanges();
        }

        public bool UpdateTeacherAccounts(IEnumerable<TeacherAccount> accounts)
        {
            _context.TeacherAccounts.UpdateRange(accounts);
            foreach (var account in accounts)
            {
                if (account.Teacher != null)
                {
                    _context.Entry(account.Teacher).State = EntityState.Detached;
                }
            }
            return SaveChanges();
        }

        public IEnumerable<TeacherAccount> GetAllTeacherAccounts()
        {
            return _context.TeacherAccounts.Include(t => t.Teacher).ToList();
        }

        public TeacherAccount GetTeacherAccountByTeacherId(int id)
        {
            var result= _context.TeacherAccounts.Where(x => x.TeacherId == id).FirstOrDefault();
            if (result!=null)
            {
                result.Teacher= _context.Teachers.Where(x => x.TeacherNumber == result.AccountName).FirstOrDefault();
            }
            return result;
        }

        public TeacherAccount GetTeacherAccountByName(string accountName)
        {
            var result = _context.TeacherAccounts.Where(x => x.AccountName == accountName).FirstOrDefault();
            if (result != null)
            {
                result.Teacher = _context.Teachers.Where(x => x.TeacherNumber == result.AccountName).FirstOrDefault();
            }
            return result;

        }
        public TeacherAccount GetTeacherAccountByTeacherNumber(string teacherNumber)
        {
            var result = _context.TeacherAccounts.Where(x => x.Teacher.TeacherNumber == teacherNumber).FirstOrDefault();
            if (result != null)
            {
                result.Teacher = _context.Teachers.Where(x => x.TeacherNumber == result.AccountName).FirstOrDefault();
            }
            return result;
        }

        public bool UpdateTeacherAccountPassWord(TeacherAccount account)
        {
            _context.TeacherAccounts.Update(account);
            return SaveChanges();
        }

        public TeacherAccount GetTeacherAccountByTeacherNum(string Num)
        {
            var result = _context.TeacherAccounts.Where(x => x.Teacher.TeacherNumber == Num).FirstOrDefault();
            if (result != null)
            {
                result.Teacher = _context.Teachers.Where(x => x.TeacherNumber == Num).FirstOrDefault();
            }
            return result;
        }
        #endregion TeacherAccount

        #region Class
        public bool AddClasses(IEnumerable<Class> classes)
        {
            _context.Classes.AddRange(classes);
            return SaveChanges();
        }

        public bool RemoveClasses(IEnumerable<int> idList)
        {
            var classes = _context.Classes.Where(x => idList.Contains(x.Id)).ToList();
            _context.Classes.RemoveRange(classes);
            return SaveChanges();
        }

        public bool UpdateClasses(IEnumerable<Class> classes)
        {
            _context.Classes.UpdateRange(classes);
            return SaveChanges();
        }

        public IEnumerable<Class> GetAllClasses()
        {
            return _context.Classes.Include(t => t.Mentor).Include(t => t.Major).ToList();
        }

        public IEnumerable<Class> GetClassesByIds(IEnumerable<int> idList)
        {
            return _context.Classes.Where(x => idList.Contains(x.Id)).Include(x=>x.Major).Include(x => x.Mentor).ToList();
        }

        public IEnumerable<Class> GetClassesByMajorIds(IEnumerable<int> idList)
        {
            return _context.Classes.Where(x => idList.Contains(x.MajorId)).Include(x => x.Major).Include(x => x.Mentor).ToList();
        }

        public IEnumerable<Class> GetClassesByMentorId(int id)
        {
            return _context.Classes.Where(x => x.TeacherId== id).Include(x => x.Major).Include(x => x.Mentor).ToList();
        }

        public IEnumerable<Class> GetClassesByTeacheNumber(string number)
        {
            return _context.Classes.Where(x => x.Mentor.TeacherNumber == number).Include(x => x.Major).Include(x => x.Mentor).ToList();
        }

        public IEnumerable<Class> GetClassesByClassInfo(ClassInfoRequestBody p)
        {
            return _context.Classes.Where(x => (p.Department==null? true: p.Department==x.Major.Department) 
            && (p.Grade == null ? true : p.Grade == x.Major.Grade)
            && (p.MajorName == null ? true : p.MajorName == x.Major.MajorName)
            && (p.ClassNumber == null ? true : p.ClassNumber == x.ClassNumber)).Include(c => c.Major).Include(c => c.Mentor).ToList();
        }
        #endregion Class

        #region Student
        public bool AddStudents(IEnumerable<Student> students)
        {
            _context.Students.AddRange(students);
            return SaveChanges();
        }

        public bool RemoveStudents(IEnumerable<int> idList)
        {
            var students = _context.Students.Where(x => idList.Contains(x.Id)).ToList();
            _context.Students.RemoveRange(students);
            return SaveChanges();
        }

        public bool UpdateStudents(IEnumerable<Student> students)
        {
            _context.Students.UpdateRange(students);
            return SaveChanges();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _context.Students.Include(t => t.Class).ThenInclude(c=>c.Mentor).Include(t => t.Major).ToList();
        }

        public IEnumerable<Student> GetStudentsByName(string name)
        {
            return _context.Students.Where(s=>s.Name== name).Include(t => t.Class).ThenInclude(c => c.Mentor).Include(t => t.Major).ToList();
        }

        public Student GetStudentByIDCardNumber(string id)
        {
            return _context.Students.Where(s => s.IdentityCardNumber == id).Include(t => t.Class).ThenInclude(c => c.Mentor).Include(t => t.Major).FirstOrDefault();
        }

        public IEnumerable<Student> GetStudentsByClassInfo(ClassInfoRequestBody request)
        {
            var classes = GetClassesByClassInfo(request);
            
            return _context.Students.Where(s => classes.Contains(s.Class)).Include(t => t.Class).ThenInclude(c => c.Mentor).Include(t => t.Major).ToList();
        }

        public IEnumerable<Student> GetStudentsByClassId(int id)
        {
            return _context.Students.Where(s => s.ClassId == id).Include(t => t.Class).ThenInclude(c => c.Mentor).Include(t => t.Major).ToList();
        }

        public IEnumerable<Student> GetStudentsByIds(IEnumerable<int> idList)
        {
            return _context.Students.Where(s => idList.Contains(s.Id)).Include(t => t.Class).ThenInclude(c => c.Mentor).Include(t => t.Major).ToList();
        }

        public Student GetStudentByStudentNumber(string data)
        {
            return _context.Students.Where(s => s.StudentNumber == data).FirstOrDefault();
        }
        #endregion Student

        #region Enroll
        public bool AddEnroll(IEnumerable<Enroll> students)
        {
            _context.Enroll.AddRange(students);
            return SaveChanges();
        }

        public bool RemoveEnroll(IEnumerable<int> idList)
        {
            var students = _context.Enroll.Where(x => idList.Contains(x.Id)).ToList();
            _context.Enroll.RemoveRange(students);
            return SaveChanges();
        }

        public bool UpdateEnroll(IEnumerable<Enroll> students)
        {
            _context.Enroll.UpdateRange(students);
            return SaveChanges();
        }

        public IEnumerable<Enroll> GetAllEnroll()
        {
            return _context.Enroll.Include(t => t.Major).ToList();
        }
        public IEnumerable<Enroll> GetEnrollByMajor(GetMajorRequestBody request)
        {
            var majors = GetMajors(request);
            return _context.Enroll.Include(t => t.Major).Where(e=> majors.Contains(e.Major)).ToList();
        }

        public Enroll GetEnrollByIDCardNumber(string id)
        {
            return _context.Enroll.Where(e => e.IdentityCardNumber == id).Include(t => t.Major).FirstOrDefault();
        }

        #endregion Enroll

        #region CourseResponsibleByTeacher
        public bool AddCourseResponsibleByTeacher(IEnumerable<CourseResponsibleByTeacher> information)
        {
            _context.CourseResponsibleByTeacher.AddRange(information);
            return SaveChanges();
        }

        public bool RemoveCourseResponsibleByTeacher(IEnumerable<int> idList)
        {
            var information = _context.CourseResponsibleByTeacher.Where(x => idList.Contains(x.Id)).ToList();
            _context.CourseResponsibleByTeacher.RemoveRange(information);
            return SaveChanges();
        }

        public bool UpdateCourseResponsibleByTeacher(IEnumerable<CourseResponsibleByTeacher> information)
        {
            _context.CourseResponsibleByTeacher.UpdateRange(information);
            return SaveChanges();
        }

        public IEnumerable<CourseResponsibleByTeacher> GetAllCourseResponsibleByTeacher()
        {
            return _context.CourseResponsibleByTeacher
                .Include(i => i.Class)
                .Include(i => i.Course)
                .Include(i => i.Teacher).ToList();
        }

        public IEnumerable<CourseResponsibleByTeacher> GetCourseAndClassByTeacherId( int id)
        {
            return _context.CourseResponsibleByTeacher.Where(c=>c.TeacherId==id)
                .Include(i => i.Class)
                .Include(i => i.Course)
                .Include(i => i.Teacher).ToList();
        }

        public CourseResponsibleByTeacher FindTeacherCourseInfo(int teacherId, int couresId, int? classId)
        {
            return _context.CourseResponsibleByTeacher.Where(c => c.TeacherId == teacherId && c.CourseId == couresId && c.ClassId== classId)
                .Include(i => i.Class)
                .Include(i => i.Course)
                .Include(i => i.Teacher).FirstOrDefault();
        }
        #endregion CourseResponsibleByTeacher

        #region CourseSchedule
        public bool AddCourseSchedule(IEnumerable<CourseSchedule> information)
        {
            information.ToList().ForEach(i=> {
                if (i.TeacherCourseInfoId == 0)
                {
                    var data = FindTeacherCourseInfo(i.TeacherCourseInfo.TeacherId, i.TeacherCourseInfo.CourseId, i.TeacherCourseInfo.ClassId);
                    if (data != null)
                    {
                        i.TeacherCourseInfoId = data.Id;
                        i.TeacherCourseInfo = null;
                    }
                    else
                    {
                        i.TeacherCourseInfo.Id = 0;
                    }
                }
                else { 
                    i.TeacherCourseInfo = null;
                }
            });
           
            _context.CourseSchedule.AddRange(information);

            var teacherCourseInfo = information.FirstOrDefault().TeacherCourseInfo;
            var classId = teacherCourseInfo.ClassId;
            if (classId > 0) {                
                var students = GetStudentsByClassId((int)classId).ToList();
                students.ForEach(s => {
                    var cs = new CourseSelection();
                    cs.StudentId = s.Id;
                    cs.CourseResponsibleByTeacherId = teacherCourseInfo.Id;
                    if (teacherCourseInfo.Id < 1) {
                        cs.TeacherCourseInfo = teacherCourseInfo;
                    }                    
                    _context.CourseSelection.AddRange(cs);
                });
            }

            return SaveChanges();
        }

        public bool RemoveCourseSchedule(IEnumerable<int> idList)
        {
            var information = _context.CourseSchedule.Where(x => idList.Contains(x.Id)).ToList();
            _context.CourseSchedule.RemoveRange(information);
            return SaveChanges();
        }

        public bool UpdateCourseSchedule(IEnumerable<CourseSchedule> information)
        {
            _context.CourseSchedule.UpdateRange(information);
            return SaveChanges();
        }

        public IEnumerable<CourseSchedule> GetAllCourseSchedule()
        {
            return _context.CourseSchedule
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i=>i.Teacher)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Course)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Class)
                .ThenInclude(i => i.Major)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Class)
                .ThenInclude(i => i.Mentor)
                .ToList();
        }

        public IEnumerable<CourseSchedule> GetCourseScheduleByTeacherId(int id)
        {
            return _context.CourseSchedule
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
            return _context.CourseSchedule
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Teacher)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Course)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Class)
                .Where(c => c.TeacherCourseInfo.ClassId == id)
                .ToList();
        }

        public IEnumerable<CourseSchedule> GetCourseScheduleByIds(IEnumerable<int> idList)
        {
            return _context.CourseSchedule
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Teacher)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Course)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Class)
                .Where(c => idList.Contains(c.Id))
                .ToList();
        }
        #endregion CourseSchedule

        #region CourseSelection
        public bool AddCourseSelection(IEnumerable<CourseSelection> information)
        {
            _context.CourseSelection.AddRange(information);
            return SaveChanges();
        }

        public bool RemoveCourseSelection(IEnumerable<int> idList)
        {
            var information = _context.CourseSelection.Where(x => idList.Contains(x.Id)).ToList();
            _context.CourseSelection.RemoveRange(information);
            return SaveChanges();
        }

        public bool UpdateCourseSelection(IEnumerable<CourseSelection> information)
        {
            _context.CourseSelection.UpdateRange(information);
            return SaveChanges();
        }

        public IEnumerable<CourseSelection> GetAllCourseSelection()
        {
            return _context.CourseSelection
                .Include(i=>i.Student)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Teacher)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Course)
                .Include(i => i.TeacherCourseInfo)
                .ThenInclude(i => i.Class)
                .ThenInclude(i => i.Major)
                .ToList();
        }

        public IEnumerable<CourseSelection> GetCourseSelectionByStudentId(int id)
        {
            return _context.CourseSelection
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
            _context.Examinations.AddRange(exams);
            return SaveChanges();
        }

        public IEnumerable<string> ImportExaminations(IEnumerable<ExaminationImportBody> exams)
        {
            var importFailedList = new List<string>();
            foreach (var exam in exams) {
                if (exam.StudentNumber.Length > 0)
                {
                    var student = GetStudentByStudentNumber(exam.StudentNumber);
                    if (student != null) {
                        var e = _mapper.Map<Examination>(exam);
                        e.StudentId = student.Id;
                        e.Student = student;
                        _context.Examinations.Add(e);
                    }
                    else
                    {
                        importFailedList.Add(exam.StudentNumber);
                    }

                }
                else {
                    importFailedList.Add(exam.StudentNumber);
                }            
            }
            SaveChanges();
            return importFailedList;            
        }

        public bool RemoveExaminations(IEnumerable<int> idList)
        {
            var exams = _context.Examinations.Where(x => idList.Contains(x.Id)).ToList();
            _context.Examinations.RemoveRange(exams);
            return SaveChanges();
        }

        public bool UpdateExaminations(IEnumerable<Examination> exams)
        {
            _context.Examinations.UpdateRange(exams);
            return SaveChanges();
        }

        public IEnumerable<Examination> GetAllExaminations()
        {
            return _context.Examinations
                .Include(i => i.Student)
                .ThenInclude(s=>s.Class)
                .ThenInclude(c=>c.Major)
                .Include(i => i.Student)
                .ThenInclude(s => s.Class)
                .ThenInclude(c => c.Mentor)
                .Include(i => i.Major)
                .Include(i => i.Course)
                .ToList();
        }

        public IEnumerable<Examination> GetExaminationsByStudentsId(IEnumerable<int> idList)
        {
            return _context.Examinations
                .Where(i=> idList.Contains(i.StudentId))
                .Include(i => i.Student)
                .Include(i => i.Major)
                .Include(i => i.Course)
                .ToList();
        }

        public IEnumerable<Examination> GetExaminationsByIds(IEnumerable<int> idList)
        {
            return _context.Examinations
                .Where(i => idList.Contains(i.Id))
                .Include(i => i.Student)
                .Include(i => i.Major)
                .Include(i => i.Course)
                .ToList();
        }
        #endregion Examination

        public TeacherAccount GetTeacherAccountByTeacherNameAndPassWord(string loginName, string password)
        {
            var result = _context.TeacherAccounts.Where(x => x.AccountName == loginName && x.Password == password && x.AccountStatus != "停用" )?.FirstOrDefault();
            if (result != null)
            {
                result.Teacher = _context.Teachers.Where(x => x.TeacherNumber == loginName)?.FirstOrDefault();
            }
            return result;
        }

        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
