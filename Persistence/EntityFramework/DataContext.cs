using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityFramework;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
            .Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<Role>()
            .Property(x => x.Name).HasColumnName("name");
        
        modelBuilder.Entity<User>()
            .Property(x => x.Id).HasColumnName("id");
        modelBuilder.Entity<User>()
            .Property(x => x.RoleId).HasColumnName("role_id");
        modelBuilder.Entity<User>()
            .Property(x => x.Email).HasColumnName("email");
        modelBuilder.Entity<User>()
            .Property(x => x.Password).HasColumnName("password");
        modelBuilder.Entity<User>()
            .Property(x => x.Name).HasColumnName("name");
        modelBuilder.Entity<User>()
            .Property(x => x.Phone).HasColumnName("phone");
        modelBuilder.Entity<User>()
            .Property(x => x.IsDeleted).HasColumnName("is_deleted");
        
        modelBuilder
            .Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique();
    }
}