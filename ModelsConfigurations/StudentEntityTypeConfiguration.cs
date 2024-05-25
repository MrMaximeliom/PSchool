using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSchool.Backend.Models;

namespace PSchool.Backend.ModelsConfigurations
{
    public class StudentEntityTypeConfiguration: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Configure Id properties
            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Configure FirstName properties
            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            // Configure LastName properties
            builder 
                .Property (x => x.LastName) 
                .IsRequired()
                .HasMaxLength(100);

            // Ignore ParentName field
            builder
                .Ignore(x => x.ParentName);
                

        }
    }
}
