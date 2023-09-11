using DomainProject.DomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InfraProject.Context
{
    public class VisitorManagementDbContext : IdentityDbContext<User>
    {
        public VisitorManagementDbContext(DbContextOptions<VisitorManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Visits> Visits { get; set; }
        public DbSet<Analytics> Analytics { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visits>()
                .HasOne(v => v.Visitor)
                .WithMany()
                .HasForeignKey(v => v.VisitorID)
                .OnDelete(DeleteBehavior.NoAction);  // or DeleteBehavior.SetNull

            modelBuilder.Entity<Visits>()
                .HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.HostEmail)
                .OnDelete(DeleteBehavior.NoAction);  // or DeleteBehavior.SetNull

            base.OnModelCreating(modelBuilder);  // for Identity configurations
        }



    }
}
