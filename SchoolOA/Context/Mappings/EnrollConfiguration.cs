using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;

namespace SchoolOA.Context.Mappings
{
    public class EnrollConfiguration : IEntityTypeConfiguration<Enroll>
    {
        public void Configure(EntityTypeBuilder<Enroll> builder)
        {
            builder.ToTable("Enroll");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasColumnType("nvarchar").HasMaxLength(100).IsRequired(true).HasColumnName("Name");
            builder.Property(p => p.Sex).HasColumnType("nvarchar").HasMaxLength(10).IsRequired(true).HasColumnName("Sex");
            builder.Property(p => p.IdentityCardNumber).HasColumnType("nvarchar").HasMaxLength(18).IsRequired(true).HasColumnName("IdentityCardNumber");
            builder.Property(p => p.HomeAddress).HasColumnType("nvarchar").HasMaxLength(500).IsRequired(true).HasColumnName("HomeAddress");
            builder.Property(p => p.PhoneNumber).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(true).HasColumnName("PhoneNumber");
            builder.Property(p => p.Portrait).HasColumnType("nvarchar").HasMaxLength(3000).IsRequired(false).HasColumnName("Portrait");
            builder.Property(p => p.MajorId).HasColumnType("int").IsRequired(true).HasColumnName("MajorId");
            builder.HasOne(p => p.Major).WithMany().HasForeignKey(e => e.MajorId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(p => p.RequestResult).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(false).HasColumnName("RequestResult");
        }
    }
}
