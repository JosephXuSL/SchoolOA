using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;

namespace SchoolOA.Context.Mappings
{
    public class TeacherAccountConfiguration : IEntityTypeConfiguration<TeacherAccount>
    {
        public void Configure(EntityTypeBuilder<TeacherAccount> builder)
        {
            builder.ToTable("TeacherAccounts");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.TeacherId).HasColumnType("int").IsRequired(true).HasColumnName("TeacherId");
            builder.HasOne(p => p.Teacher).WithOne().HasForeignKey<TeacherAccount>(t=>t.TeacherId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(p => p.AccountName).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("AccountName");
            builder.Property(p => p.Password).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(true).HasColumnName("Password");
            builder.Property(p => p.AccountStatus).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(false).HasColumnName("AccountStatus");            
            builder.Property(p => p.IsMentorAccount).HasColumnType("bit").HasColumnName("IsMentorAccount");
        }
    }
}
