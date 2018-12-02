using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlannyBackend.Model;
using PlannyBackend.Model.Identity;
using PlannyBackend.DAL.Configure;

namespace PlannyBackend.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public virtual DbSet<Participation> Participations { get; set; }
        public virtual DbSet<Planny> Plannies { get; set; }
        public virtual DbSet<PlannyCategory> PlannyCategories { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 

            #region Identity_Table_Names
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");

            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
            #endregion

            builder
              .ApplyConfiguration(new ParticipationConfiguration())
              .ApplyConfiguration(new PlannyCategoryConfiguration());

        }       
    }
}
