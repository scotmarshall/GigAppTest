using Microsoft.EntityFrameworkCore;

namespace GigAppTest.Models;

public class VenueContext : DbContext
{
    public VenueContext(DbContextOptions<VenueContext> options)
        : base(options)
    {
    }

    public DbSet<VenueItem> VenueItems { get; set; } = null!;
}