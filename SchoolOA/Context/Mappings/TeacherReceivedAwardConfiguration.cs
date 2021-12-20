using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;

namespace SchoolOA.Context.Mappings
{
    public class TeacherReceivedAwardConfiguration : IEntityTypeConfiguration<TeacherReceivedAward>
    {
        public void Configure(EntityTypeBuilder<TeacherReceivedAward> builder)
        {
            builder.ToTable("TeacherReceivedAward");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.AwardName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("AwardName");
            builder.Property(p => p.AwardDate).HasColumnType("datetime").IsRequired(true).HasColumnName("AwardDate");
            builder.Property(p => p.Detail).HasColumnType("nvarchar").HasMaxLength(1000).IsRequired(false).HasColumnName("Detail");
            builder.Property(p => p.TeacherId).HasColumnType("int").IsRequired(true).HasColumnName("TeacherId");
            builder.HasOne(p => p.Teacher).WithMany().HasForeignKey(e => e.TeacherId).OnDelete(DeleteBehavior.ClientSetNull);            
        }
    }
}
