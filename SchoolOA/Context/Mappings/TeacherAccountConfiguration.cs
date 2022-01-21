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
            builder.Property(p => p.IsAdminAccount).HasColumnType("bit").HasColumnName("IsAdminAccount");
            builder.Ignore(p => p.AccessFailedCount);
            builder.Ignore(p => p.ConcurrencyStamp);
            builder.Ignore(p => p.Email);
            builder.Ignore(p => p.EmailConfirmed);
            builder.Ignore(p => p.LockoutEnabled);
            builder.Ignore(p => p.LockoutEnd);
            builder.Ignore(p => p.NormalizedEmail);
            builder.Ignore(p => p.NormalizedUserName);
            builder.Ignore(p => p.PasswordHash);
            builder.Ignore(p => p.PhoneNumber);
            builder.Ignore(p => p.PhoneNumberConfirmed);
            builder.Ignore(p => p.SecurityStamp);
            builder.Ignore(p => p.TwoFactorEnabled);
            builder.Ignore(p => p.UserName);
        }
    }
}
