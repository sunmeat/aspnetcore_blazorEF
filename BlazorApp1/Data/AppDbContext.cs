using Microsoft.EntityFrameworkCore;
using BlazorApp1.Models;

namespace BlazorApp1.Data;

public class AppDbContext : DbContext
{
    public DbSet<ContactInfoEntity> ContactInfos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // опціонально: конфігурація, індекси тощо
        // modelBuilder.Entity<ContactInfoEntity>() // не розкоментовано, щоб дозволити дублікати email
        //    .HasIndex(e => e.Email)
        //    .IsUnique();
    }
}