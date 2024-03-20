using ManagerAccount.Entities;
using ManagerAccount.Models.Requests;
using Microsoft.EntityFrameworkCore;

namespace ManagerAccount.DataAccess;

public class AppDbContext : DbContext
{
    internal DbSet<User> Users { get; set; }
    internal DbSet<Manager> Managers { get; set; }
    internal DbSet<Client> Clients { get; set; }
    
    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().UseTptMappingStrategy();

        modelBuilder.Entity<Client>()
            .Property(cl => cl.Email)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(cl => cl.Login)
            .IsRequired();
        
        modelBuilder.Entity<User>()
            .Property(cl => cl.Password)
            .IsRequired();
        
        modelBuilder.Entity<Manager>()
            .Property(cl => cl.Role)
            .HasDefaultValue(Roles.Manager);
        
        modelBuilder.Entity<Client>()
            .Property(cl => cl.Role)
            .HasDefaultValue(Roles.Client);
        
        
        base.OnModelCreating(modelBuilder);
    }
}