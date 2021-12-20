using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;

namespace SchoolOA.Context.Mappings
{
    public class MajorConfiguration : IEntityTypeConfiguration<Major>
    {
        public void Configure(EntityTypeBuilder<Major> builder)
        {
            builder.ToTable("Majors");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(m => m.Department).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("Department");
            builder.Property(m => m.Grade).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("Grade");
            builder.Property(m => m.MajorName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("MajorName");
        }
    }
}
