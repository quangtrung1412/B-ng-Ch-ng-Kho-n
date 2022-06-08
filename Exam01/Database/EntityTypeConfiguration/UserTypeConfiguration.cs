using Exam01.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Exam01.Database.EntityTypeConfiguration;
public class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
       builder.ToTable("User")
       .HasKey(e=>e.Id)
       .HasName("User_Id_Pk");
       builder.HasIndex(e => e.Email)
       .IsUnique()
       .HasDatabaseName("User_Email_Uq");
       builder.Property(e=>e.Name)
       .HasColumnName("Name")
       .HasMaxLength(200);
       builder.Property(e=>e.Email)
       .HasColumnName("Email")
       .HasMaxLength(200)
       .IsRequired();
       builder.Property(e=>e.Password)
       .HasColumnName("Password")
       .HasMaxLength(200)
       .IsRequired();
       builder.Property(e=>e.TemPass)
       .HasColumnName("TemPass")
       .HasMaxLength(200)
       .IsRequired();
       builder.Property(e=>e.Avatar)
       .HasColumnName("Avatar");
    }
}
