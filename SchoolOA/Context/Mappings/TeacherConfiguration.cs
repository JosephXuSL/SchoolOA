using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;

namespace SchoolOA.Context.Mappings
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teachers");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("Name");
            builder.Property(p => p.TeacherNumber).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(true).HasColumnName("TeacherNumber");
            builder.Property(p => p.TeacherStatus).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(false).HasColumnName("TeacherStatus");
            builder.Property(p => p.TeacherComment).HasColumnType("nvarchar").HasMaxLength(1000).IsRequired(false).HasColumnName("TeacherComment");
            builder.Property(p => p.PhoneNumber).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(false).HasColumnName("PhoneNumber");
            builder.Property(p => p.IsMentor).HasColumnType("bit").HasColumnName("IsMentor");
        }
    }
}
