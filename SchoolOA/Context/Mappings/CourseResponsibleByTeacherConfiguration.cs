using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;

namespace SchoolOA.Context.Mappings
{
    public class CourseResponsibleByTeacherConfiguration : IEntityTypeConfiguration<CourseResponsibleByTeacher>
    {
        public void Configure(EntityTypeBuilder<CourseResponsibleByTeacher> builder)
        {
            builder.ToTable("CourseResponsibleByTeacher");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();            
            builder.Property(p => p.TeacherId).HasColumnType("int").IsRequired(true).HasColumnName("TeacherId");
            builder.HasOne(p => p.Teacher).WithMany().HasForeignKey(e => e.TeacherId).OnDelete(DeleteBehavior.ClientSetNull);            
            builder.Property(p => p.CourseId).HasColumnType("int").IsRequired(true).HasColumnName("CourseId");
            builder.HasOne(p => p.Course).WithMany().HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(p => p.ClassId).HasColumnType("int").IsRequired(false).HasColumnName("ClassId");
            builder.HasOne(p => p.Class).WithMany().HasForeignKey(e => e.ClassId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(p => p.Semester).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("Semester");
        }
    }
}
