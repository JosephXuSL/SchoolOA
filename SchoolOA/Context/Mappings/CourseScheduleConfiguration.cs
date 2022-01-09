using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;

namespace SchoolOA.Context.Mappings
{
    public class CourseScheduleConfiguration : IEntityTypeConfiguration<CourseSchedule>
    {
        public void Configure(EntityTypeBuilder<CourseSchedule> builder)
        {
            builder.ToTable("CourseSchedule");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.TeacherCourseInfoId).HasColumnType("int").IsRequired(true).HasColumnName("CourseResponsibleByTeacherId");
            builder.HasOne(p => p.TeacherCourseInfo).WithMany().HasForeignKey(e => e.TeacherCourseInfoId).OnDelete(DeleteBehavior.ClientSetNull);            
            builder.Property(p => p.ScheduledWeekday).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(true).HasColumnName("ScheduledWeekday");
            builder.Property(p => p.ScheduledTime).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(true).HasColumnName("ScheduledTime");
        }
    }
}
