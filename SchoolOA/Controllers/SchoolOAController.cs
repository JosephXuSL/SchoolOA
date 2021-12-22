﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolOA.Entities;
using SchoolOA.Repositories;
using SchoolOA.Services;
using SchoolOA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[action]")]
    public class SchoolOAController : ControllerBase
    {

        private readonly SchoolRepository _rep;
        private readonly ILogger<SchoolOAController> _logger;
        private readonly PictureService _pic;
        private readonly IMapper _mapper;

        public SchoolOAController(SchoolRepository rep, PictureService pic, ILogger<SchoolOAController> logger, IMapper mapper)
        {
            _rep = rep;
            _logger = logger;
            _pic = pic;
            _mapper = mapper;
        }
       
        #region Course
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddCourses([FromBody] IEnumerable<CourseRequestBody> request)
        {
            try 
            {                
                if (ModelState.IsValid)
                {
                    var courses = _mapper.Map<IEnumerable<Course>>(request);
                    if (_rep.AddCourses(courses))
                    {
                        return Created("Saved success", courses);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveCourses([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveCourses(idList)) {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateCourses([FromBody] IEnumerable<CourseRequestBody> courses)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_rep.UpdateCourses(_mapper.Map<IEnumerable<Course>>(courses)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Course>), 200)]
        public IActionResult GetAllCourses()
        {
            try
            {
                var result = _rep.GetAllCourses();
                if (result.Count()>0)
                {
                    return Ok(result);
                }
                else {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Course>),200)]
        public IActionResult GetCoursesByIds([FromBody] IEnumerable<int> idList)
        {
            try
            {
                var result = _rep.GetCourseByIds(idList);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No items found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }
        #endregion Course

        #region Teacher
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddTeachers([FromBody] IEnumerable<TeacherRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var teachers = _mapper.Map<IEnumerable<Teacher>>(request);
                    if (_rep.AddTeachers(teachers))
                    {
                        return Created("Saved success", teachers);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveTeachers([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveTeachers(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateTeachers([FromBody] IEnumerable<TeacherRequestBody> teachers)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_rep.UpdateTeachers(_mapper.Map<IEnumerable<Teacher>>(teachers)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Teacher>), 200)]
        public IActionResult GetAllTeachers()
        {
            try
            {
                var result = _rep.GetAllTeachers();
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpGet("{Name:}")]
        [ProducesResponseType(typeof(IEnumerable<Teacher>), 200)]
        public IActionResult FindTeachersByName(string name)
        {
            try
            {
                var result = _rep.GetTeachersByName(name);
                if (result.Count()>0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Items Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Teacher>), 200)]
        public IActionResult GetTeacherByIds([FromBody] IEnumerable<int> idList)
        {
            try
            {
                var result = _rep.GetTeacherByIds(idList);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No items found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }
        #endregion Teacher

        #region Major
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddMajors([FromBody] IEnumerable<MajorRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var majors = _mapper.Map<IEnumerable<Major>>(request);
                    if (_rep.AddMajors(majors))
                    {
                        return Created("Saved success", majors);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveMajors([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveMajors(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateMajors([FromBody] IEnumerable<MajorRequestBody> majors)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_rep.UpdateMajors(_mapper.Map<IEnumerable<Major>>(majors)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Major>), 200)]
        public IActionResult GetAllMajors()
        {
            try
            {
                var result = _rep.GetAllMajors();
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpGet("{grade:}")]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public IActionResult GetDepartmentByGrade(string grade)
        {
            try
            {
                var result = _rep.GetDepartmentByGrade(grade);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("no data from table");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        public IActionResult GetMajorNameByGradeAndDepartment(string grade, string department)
        {
            try
            {
                var result = _rep.GetMajorNameByGradeAndDepartment(grade, department);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("no data from table");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Major>), 200)]
        public IActionResult GetMajors([FromBody] GetMajorRequestBody request)
        {
            try
            {
                var result = _rep.GetMajors(request);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("no data from table");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Major>), 200)]
        public IActionResult GetMajorByIds([FromBody] IEnumerable<int> idList)
        {
            try
            {
                var result = _rep.GetMajorByIds(idList);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No items found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }
        #endregion Major

        #region TeacherReceivedAward
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddTeacherReceivedAward([FromBody] IEnumerable<TeacherReceivedAwardRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var awards = _mapper.Map<IEnumerable<TeacherReceivedAward>>(request);
                    if (_rep.AddTeacherReceivedAward(awards))
                    {
                        return Created("Saved success", awards);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveTeacherReceivedAward([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveTeacherReceivedAward(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateTeacherReceivedAward([FromBody] IEnumerable<TeacherReceivedAwardRequestBody> request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_rep.UpdateTeacherReceivedAward(_mapper.Map<IEnumerable<TeacherReceivedAward>>(request)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TeacherReceivedAward>), 200)]
        public IActionResult GetAllTeacherReceivedAward()
        {
            try
            {
                var result = _rep.GetAllTeacherReceivedAward();
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpGet("{id:}")]
        [ProducesResponseType(typeof(IEnumerable<TeacherReceivedAward>), 200)]
        public IActionResult GetTeacherReceivedAwardByTeacherId(int id)
        {
            try
            {
                var result = _rep.GetTeacherReceivedAwardByTeacherId(id);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No items found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }
        #endregion TeacherReceivedAward

        #region TeacherAccount
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddTeacherAccounts([FromBody] IEnumerable<TeacherAccountRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var accounts = _mapper.Map<IEnumerable<TeacherAccount>>(request);
                    if (_rep.AddTeacherAccounts(accounts))
                    {
                        return Created("Saved success", accounts);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveTeacherAccounts([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveTeacherAccounts(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateTeacherAccounts([FromBody] IEnumerable<TeacherAccountRequestBody> request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_rep.UpdateTeacherAccounts(_mapper.Map<IEnumerable<TeacherAccount>>(request)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TeacherAccount>), 200)]
        public IActionResult GetAllTeacherAccounts()
        {
            try
            {
                var result = _rep.GetAllTeacherAccounts();
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpGet("{id:}")]
        [ProducesResponseType(typeof(TeacherAccount), 200)]
        public IActionResult GetTeacherAccountByTeacherId(int id)
        {
            try
            {
                var result = _rep.GetTeacherAccountByTeacherId(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No items found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }
        #endregion TeacherAccount

        #region Class
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddClasses([FromBody] IEnumerable<ClassRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var classes = _mapper.Map<IEnumerable<Class>>(request);
                    if (_rep.AddClasses(classes))
                    {
                        return Created("Saved success", classes);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveClasses([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveClasses(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateClasses([FromBody] IEnumerable<ClassRequestBody> classes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_rep.UpdateClasses(_mapper.Map<IEnumerable<Class>>(classes)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Class>), 200)]
        public IActionResult GetAllClasses()
        {
            try
            {
                var result = _rep.GetAllClasses();
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Class>), 200)]
        public IActionResult GetClassesByIds([FromBody] IEnumerable<int> idList)
        {
            try
            {
                var result = _rep.GetClassesByIds(idList);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No items found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Class>), 200)]
        public IActionResult GetClassesByMajorIds([FromBody] IEnumerable<int> idList)
        {
            try
            {
                var result = _rep.GetClassesByMajorIds(idList);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No items found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpGet("{id:}")]
        [ProducesResponseType(typeof(IEnumerable<Class>), 200)]
        public IActionResult GetClassesByMentorId(int id)
        {
            try
            {
                var result = _rep.GetClassesByMentorId(id);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No items found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }
        #endregion Class

        #region Student
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddStudents([FromBody] IEnumerable<StudentRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    foreach (var student in request)
                    {
                        if (student.Portrait != null)
                        {
                            student.Portrait = _pic.SavePhoto(student.Portrait, student.Name, student.IdentityCardNumber);
                        }
                    }

                    var students = _mapper.Map<IEnumerable<Student>>(request);
                    
                    if (_rep.AddStudents(students))
                    {
                        return Created("Saved success", students);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveStudents([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveStudents(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateStudents([FromBody] IEnumerable<StudentRequestBody> students)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var student in students)
                    {
                        if (student.Portrait != null)
                        {
                            student.Portrait = _pic.SavePhoto(student.Portrait, student.Name, student.IdentityCardNumber);
                        }
                    }
                    if (_rep.UpdateStudents(_mapper.Map<IEnumerable<Student>>(students)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Student>), 200)]
        public IActionResult GetAllStudents()
        {
            try
            {
                var result = _rep.GetAllStudents();
                if (result.Count() > 0)
                {
                    foreach (var student in result)
                    {
                        if (student.Portrait != null)
                        {
                            student.Portrait = _pic.GetPhoto(student.Portrait);
                        }
                    }
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        [HttpGet("{name:}")]
        [ProducesResponseType(typeof(IEnumerable<Student>), 200)]
        public IActionResult GetStudentsByName(string name)
        {
            try
            {
                var result = _rep.GetStudentsByName(name);
                if (result.Count()>0)
                {
                    foreach (var student in result)
                    {
                        if (student.Portrait != null)
                        {
                            student.Portrait = _pic.GetPhoto(student.Portrait);
                        }
                    }
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Items Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }

        [HttpGet("{number:}")]
        [ProducesResponseType(typeof(Student), 200)]
        public IActionResult GetStudentByIDCardNumber(string number)
        {
            try
            {
                var result = _rep.GetStudentByIDCardNumber(number);
                if (result != null)
                {                    
                    if (result.Portrait != null)
                    {
                        result.Portrait = _pic.GetPhoto(result.Portrait);
                    }                    
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Items Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Student>), 200)]
        public IActionResult GetStudentsByClassInfo([FromBody] ClassInfoRequestBody rquest)
        {
            try
            {
                var result = _rep.GetStudentsByClassInfo(rquest);
                if (result.Count() > 0)
                {
                    foreach (var student in result)
                    {
                        if (student.Portrait != null)
                        {
                            student.Portrait = _pic.GetPhoto(student.Portrait);
                        }
                    }
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Items Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }

        }

        #endregion Student

        #region Enroll
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddEnroll([FromBody] IEnumerable<EnrollRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    foreach (var student in request)
                    {
                        if (student.Portrait != null)
                        {
                            student.Portrait = _pic.SavePhoto(student.Portrait, student.Name, student.IdentityCardNumber);
                        }
                    }
                    var students = _mapper.Map<IEnumerable<Enroll>>(request);
                    if (_rep.AddEnroll(students))
                    {
                        return Created("Saved success", students);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveEnroll([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveEnroll(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateEnroll([FromBody] IEnumerable<EnrollRequestBody> students)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var student in students)
                    {
                        if (student.Portrait != null)
                        {
                            student.Portrait = _pic.SavePhoto(student.Portrait, student.Name, student.IdentityCardNumber);
                        }
                    }

                    if (_rep.UpdateEnroll(_mapper.Map<IEnumerable<Enroll>>(students)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Enroll>), 200)]
        public IActionResult GetAllEnroll()
        {
            try
            {
                var result = _rep.GetAllEnroll();
                if (result.Count() >0)
                {
                    foreach (var student in result)
                    {
                        if (student.Portrait != null)
                        {
                            student.Portrait = _pic.GetPhoto(student.Portrait);
                        }
                    }
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Enroll>), 200)]
        public IActionResult GetEnrollByMajor([FromBody] GetMajorRequestBody request)
        {
            try
            {
                var result = _rep.GetEnrollByMajor(request);
                if (result.Count()>0)
                {
                    foreach (var student in result)
                    {
                        if (student.Portrait != null)
                        {
                            student.Portrait = _pic.GetPhoto(student.Portrait);
                        }
                    }
                    return Ok(result);
                }
                else
                {
                    return NotFound("No items found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }

        [HttpGet("{number:}")]
        [ProducesResponseType(typeof(Student), 200)]
        public IActionResult GetEnrollByIDCardNumber(string number)
        {
            try
            {
                var result = _rep.GetEnrollByIDCardNumber(number);
                if (result != null)
                {
                    if (result.Portrait != null)
                    {
                        result.Portrait = _pic.GetPhoto(result.Portrait);
                    }
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Items Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }
        #endregion Enroll

        #region CourseResponsibleByTeacher
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddCourseResponsibleByTeacher([FromBody] IEnumerable<CourseResponsibleByTeacherRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var information = _mapper.Map<IEnumerable<CourseResponsibleByTeacher>>(request);
                    if (_rep.AddCourseResponsibleByTeacher(information))
                    {
                        return Created("Saved success", information);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveCourseResponsibleByTeacher([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveCourseResponsibleByTeacher(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateCourseResponsibleByTeacher([FromBody] IEnumerable<CourseResponsibleByTeacherRequestBody> information)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_rep.UpdateCourseResponsibleByTeacher(_mapper.Map<IEnumerable<CourseResponsibleByTeacher>>(information)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseResponsibleByTeacher>), 200)]
        public IActionResult GetAllCourseResponsibleByTeacher()
        {
            try
            {
                var result = _rep.GetAllCourseResponsibleByTeacher();
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }

        [HttpGet("{id:}")]
        [ProducesResponseType(typeof(IEnumerable<CourseResponsibleByTeacher>), 200)]
        public IActionResult GetCourseAndClassByTeacherId(int id)
        {
            try
            {
                var result = _rep.GetCourseAndClassByTeacherId(id);
                if (result.Count()>0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Items Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }

        #endregion CourseResponsibleByTeacher

        #region CourseSchedule
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddCourseSchedule([FromBody] IEnumerable<CourseScheduleRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var information = _mapper.Map<IEnumerable<CourseSchedule>>(request);
                    if (_rep.AddCourseSchedule(information))
                    {
                        return Created("Saved success", information);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveCourseSchedule([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveCourseSchedule(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateCourseSchedule([FromBody] IEnumerable<CourseScheduleRequestBody> information)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_rep.UpdateCourseSchedule(_mapper.Map<IEnumerable<CourseSchedule>>(information)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseSchedule>), 200)]
        public IActionResult GetAllCourseSchedule()
        {
            try
            {
                var result = _rep.GetAllCourseSchedule();
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }

        [HttpGet("{teacherId:}")]
        [ProducesResponseType(typeof(IEnumerable<CourseSchedule>), 200)]
        public IActionResult GetCourseScheduleByTeacherId(int teacherId)
        {
            try
            {
                var result = _rep.GetCourseScheduleByTeacherId(teacherId);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Items Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }

        [HttpGet("{classId:}")]
        [ProducesResponseType(typeof(IEnumerable<CourseSchedule>), 200)]
        public IActionResult GetCourseScheduleByClassId(int classId)
        {
            try
            {
                var result = _rep.GetCourseScheduleByClassId(classId);
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Items Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }
        #endregion CourseSchedule

        #region CourseSelection
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddCourseSelection([FromBody] IEnumerable<CourseSelectionRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var information = _mapper.Map<IEnumerable<CourseSelection>>(request);
                    if (_rep.AddCourseSelection(information))
                    {
                        return Created("Saved success", information);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveCourseSelection([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveCourseSelection(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateCourseSelection([FromBody] IEnumerable<CourseSelectionRequestBody> information)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_rep.UpdateCourseSelection(_mapper.Map<IEnumerable<CourseSelection>>(information)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseSelection>), 200)]
        public IActionResult GetAllCourseSelection()
        {
            try
            {
                var result = _rep.GetAllCourseSelection();
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }

        [HttpGet("{studnetId:}")]
        [ProducesResponseType(typeof(IEnumerable<CourseSelection>), 200)]
        public IActionResult GetCourseSelectionByStudentId(int studnetId)
        {
            try
            {
                var result = _rep.GetCourseSelectionByStudentId(studnetId);
                if (result.Count()>0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Items Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }
        #endregion CourseSelection

        #region Examination
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult AddExaminations([FromBody] IEnumerable<ExaminationRequestBody> request)
        {
            try
            {                
                if (ModelState.IsValid)
                {
                    var exams = _mapper.Map<IEnumerable<Examination>>(request);
                    if (_rep.AddExaminations(exams))
                    {
                        return Created("Saved success", exams);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult RemoveExaminations([FromBody] IEnumerable<int> idList)
        {
            try
            {
                if (_rep.RemoveExaminations(idList))
                {
                    return Ok("Deleted success");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult UpdateExaminations([FromBody] IEnumerable<ExaminationRequestBody> exams)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_rep.UpdateExaminations(_mapper.Map<IEnumerable<Examination>>(exams)))
                    {
                        return Ok("Saved success");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
            }
            return BadRequest("Some error makes request failed");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Examination>), 200)]
        public IActionResult GetAllExaminations()
        {
            try
            {
                var result = _rep.GetAllExaminations();
                if (result.Count() > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("Table is empty");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Examination>), 200)]
        public IActionResult GetExaminationsByStudentIds(IEnumerable<int> idList)
        {
            try
            {
                var result = _rep.GetExaminationsByStudentsId(idList);
                if (result.Count()>0)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("No Items Found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed due to : {ex}");
                return BadRequest("Some error makes request failed");
            }
        }
        #endregion Examination

    }
}