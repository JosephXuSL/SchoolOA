using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;

namespace SchoolOA.Context.Mappings
{
    public class ExaminationConfiguration : IEntityTypeConfiguration<Examination>
    {
        public void Configure(EntityTypeBuilder<Examination> builder)
        {
            builder.ToTable("Examinations");
            builder.HasKey(p => p.Id);            
            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.Semester).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("Semester");
            builder.Property(p => p.CourseId).HasColumnType("int").IsRequired(true).HasColumnName("CourseId");
            builder.HasOne(p => p.Course).WithMany().HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(p => p.MajorId).HasColumnType("int").IsRequired(true).HasColumnName("MajorId");
            builder.HasOne(p => p.Major).WithMany().HasForeignKey(e => e.MajorId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(p => p.StudentId).HasColumnType("int").IsRequired(true).HasColumnName("StudentId");
            builder.HasOne(p => p.Student).WithMany().HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.ClientSetNull);
            //builder.Property(p => p.ExamSubject).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("ExamSubject");
            //builder.Property(p => p.ExamDate).HasColumnType("datetime").IsRequired(true).HasColumnName("ExamDate");
            //builder.Property(p => p.CourseResponsibleByTeacherId).HasColumnType("int").IsRequired(false).HasColumnName("CourseId");
            //builder.HasOne(p => p.TeacherCourseInfo).WithMany().HasForeignKey(e => e.CourseResponsibleByTeacherId).OnDelete(DeleteBehavior.ClientSetNull); 
            //builder.Property(p => p.Perormance).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(false).HasColumnName("Perormance");
            builder.Property(p => p.Score).HasColumnType("decimal(5, 2)").IsRequired(true).HasColumnName("Score");
        }
    }
}
