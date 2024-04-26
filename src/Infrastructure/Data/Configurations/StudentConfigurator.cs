
using ca.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ca.Infrastructure.Data.Configurations;
public class StudentConfigurator: IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(t => t.FirstName)
           .HasMaxLength(250)
           .IsRequired();

        builder.Property(t => t.LastName)
           .IsRequired();

    }
}
