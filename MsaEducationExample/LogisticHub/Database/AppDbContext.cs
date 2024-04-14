using LogisticHub.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogisticHub.Database;

public class AppDbContext : DbContext
{
    internal DbSet<Order> Orders { get; set; }
    internal DbSet<Tanker> Tankers { get; set; }

    public AppDbContext()
    {
        
    }

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
        
    }
}