using EngineerProject.API.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EngineerProject.API.Entities
{
    public class EngineerContext : DbContext
    {
        public EngineerContext(DbContextOptions<EngineerContext> options) : base(options)
        {
#if Release
            if (Database.GetPendingMigrations().Any())
                Database.Migrate();
#endif
        }

        public DbSet<File> Files { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>().HasOne(a => a.User).WithMany(a => a.Comments).OnDelete(DeleteBehavior.NoAction);
        }
    }
}