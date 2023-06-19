using Microsoft.EntityFrameworkCore;

namespace GigAppTest.Models;

public class ArtistContext : DbContext
{
    public ArtistContext(DbContextOptions<ArtistContext> options)
        : base(options)
    {
    }

    public DbSet<ArtistItem> ArtistItems { get; set; } = null!;
}