using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PSchool.Backend.Models;
using PSchool.Backend.ModelsConfigurations;
using System.Reflection.Metadata;

namespace PSchool.Backend
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Change default identity tables names
            builder.Entity<User>()
                .ToTable("users")
                .HasIndex(p => p.PhoneNumber)
                .IsUnique();
            builder.Entity<IdentityRole>().ToTable("roles").HasData(
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "User", ConcurrencyStamp = "2", NormalizedName = "USER" }
                );
            builder.Entity<IdentityUserRole<string>>().ToTable("userRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("userClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("userLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("roleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("userTokens");
            

    /*        builder.Entity<User>()
           .HasOne(e => e.Parent)
            .WithOne(e => e.User)
               .HasForeignKey<Parent>(e => e.UserId)
               .IsRequired();*/

            // Configure User properties
            new UserEntityTypeConfiguration().Configure(builder.Entity<User>()); 

            // Configure Student properties
            new StudentEntityTypeConfiguration().Configure(builder.Entity<Student>());

            // Configure Parent properties
            new ParentEntityTypeConfiguration().Configure(builder.Entity<Parent>());    

        }

        // Declare User model
        public DbSet<User> Users { get; set; }

        // Declare Parent model
        public DbSet<Parent> Parents { get; set; }

        // Declare Student model
        public DbSet<Student> Students { get; set; }

    }
}
