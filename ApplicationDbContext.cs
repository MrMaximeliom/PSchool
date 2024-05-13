using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PSchool.Backend.Models;
using PSchool.Backend.ModelsConfigurations;

namespace PSchool.Backend
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Change default identity tables names
            builder.Entity<User>().ToTable("users");
            builder.Entity<IdentityRole>().ToTable("roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("userRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("userClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("userLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("roleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("userTokens");

            // Configure Student and Parent relation
            builder.Entity<Student>()
                .HasOne(p => p.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(p => p.ParentId);

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
