using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSchool.Backend.Models;

namespace PSchool.Backend.ModelsConfigurations
{
    public class ParentEntityTypeConfiguration : IEntityTypeConfiguration<Parent>

    {
        public void Configure(EntityTypeBuilder<Parent> builder) 
        {
            // Configure Id properties
            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            // Configure WorkPhone properties
            builder
                .Property(x => x.WorkPhone)
                .IsRequired(false)
                .HasMaxLength(25);

            // Configure HomeAddress properties
            builder 
                .Property(x => x.HomeAddress)
                .IsRequired(false)
                .HasMaxLength(300);

            // Configure Siblings properties
            builder
                .Property(x => x.Siblings)
                .IsRequired();



            
   
        
        
                
        
        }
    }
}
