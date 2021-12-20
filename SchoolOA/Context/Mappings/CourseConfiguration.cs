using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;

namespace SchoolOA.Context.Mappings
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Courses");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.CourseName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("CourseName");
            builder.Property(p => p.Textbook).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(false).HasColumnName("Textbook");           
        }
    }
}
