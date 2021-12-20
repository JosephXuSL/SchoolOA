using Microsoft.EntityFrameworkCore;
using SchoolOA.Context.Mappings;
using SchoolOA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Context
{
    public class SchoolContext:DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseSchedule> CourseSchedule { get; set; }
        public DbSet<CourseSelection> CourseSelection { get; set; }
        public DbSet<Enroll> Enroll { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherAccount> TeacherAccounts { get; set; }
        public DbSet<CourseResponsibleByTeacher> CourseResponsibleByTeacher { get; set; }
        public DbSet<TeacherReceivedAward> TeacherReceivedAward { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer();
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClassConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new CourseScheduleConfiguration());
            modelBuilder.ApplyConfiguration(new CourseSelectionConfiguration());
            modelBuilder.ApplyConfiguration(new EnrollConfiguration());
            modelBuilder.ApplyConfiguration(new ExaminationConfiguration());
            modelBuilder.ApplyConfiguration(new MajorConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherAccountConfiguration());
            modelBuilder.ApplyConfiguration(new CourseResponsibleByTeacherConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherConfiguration());
            modelBuilder.ApplyConfiguration(new TeacherReceivedAwardConfiguration());
        }

    }
}
