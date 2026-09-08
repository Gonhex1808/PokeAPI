using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pokemon> Pokemon => Set<Pokemon>();
}