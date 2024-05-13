using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSchool.Backend.Models;

namespace PSchool.Backend.ModelsConfigurations
{
    public class UserEntityTypeConfiguration:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
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
                .HasMaxLength(150);

            // Configure LastName properties
            builder
                .Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(150);

            // Configure Email properties
            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            // Configure PhoneNumber properties
            builder
                .Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(25);

            // Configure Username properties
            builder
                .Property(x => x.UserName)
                .IsRequired(false)
                .HasMaxLength(30);

        
        }

    }
}
