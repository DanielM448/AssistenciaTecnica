using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Db.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoleModel>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRoleModel>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRoleModel>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            // Adicionar dados iniciais
            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel { Id = 1, RoleName = "Admin" },
                new RoleModel { Id = 2, RoleName = "Tecnichian" },
                new RoleModel { Id = 3, RoleName = "Client" }
            );
        }
    }
}
