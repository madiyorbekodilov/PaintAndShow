using Microsoft.EntityFrameworkCore;
using PaintAndShow.Domain.Entities;

namespace PaintAndShow.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Friend> Friends { get; set; }
}
