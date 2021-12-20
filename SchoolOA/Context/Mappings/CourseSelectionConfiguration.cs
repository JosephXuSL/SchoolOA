using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;

namespace SchoolOA.Context.Mappings
{
    public class CourseSelectionConfiguration : IEntityTypeConfiguration<CourseSelection>
    {
        public void Configure(EntityTypeBuilder<CourseSelection> builder)
        {
            builder.ToTable("CourseSelection");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.StudentId).HasColumnType("int").IsRequired(true).HasColumnName("StudentId");
            builder.HasOne(p => p.Student).WithMany().HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(p => p.CourseResponsibleByTeacherId).HasColumnType("int").IsRequired(true).HasColumnName("CourseId");
            builder.HasOne(p => p.TeacherCourseInfo).WithMany().HasForeignKey(e => e.CourseResponsibleByTeacherId).OnDelete(DeleteBehavior.ClientSetNull);           
        }
    }
}
