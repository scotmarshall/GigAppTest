using Microsoft.EntityFrameworkCore;

namespace GigAppTest.Models;

public class GigContext : DbContext
{
    public GigContext(DbContextOptions<GigContext> options)
        : base(options)
    {
    }

    public DbSet<GigItem> GigItems { get; set; } = null!;
}