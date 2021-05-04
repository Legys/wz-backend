using Microsoft.EntityFrameworkCore;

namespace WzBeatsApi.Models
{
  public class WzBeatsApiContext : DbContext
  {
    public WzBeatsApiContext(DbContextOptions<WzBeatsApiContext> options)
        : base(options)
    {
    }

    public DbSet<TrackItem> TrackItems { get; set; }
    public DbSet<AssetItem> AssetItems { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.EnableSensitiveDataLogging();
    }
  }
}
