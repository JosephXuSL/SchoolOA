using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolOA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolOA.Context.Mappings
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Classes");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnType("int").HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(p => p.ClassNumber).HasColumnType("nvarchar").HasMaxLength(20).IsRequired(true).HasColumnName("ClassNumber");
            builder.Property(p => p.MajorId).HasColumnType("int").IsRequired(true).HasColumnName("MajorId");
            builder.HasOne(p => p.Major).WithMany().HasForeignKey(c=>c.MajorId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.Property(p => p.TeacherId).HasColumnType("int").IsRequired(true).HasColumnName("MentorId");
            builder.HasOne(p => p.Mentor).WithMany().HasForeignKey(c=>c.TeacherId).OnDelete(DeleteBehavior.ClientSetNull);
            
        }
    }
}
