using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WzBeatsApi.Models
{
  public class WzBeatsApiContext : DbContext
  {

    private IConfiguration configuration;

    public WzBeatsApiContext(DbContextOptions<WzBeatsApiContext> options, IConfiguration configuration)
        : base(options)
    {
      this.configuration = configuration;
    }

    public DbSet<TrackItem> TrackItems { get; set; }
    public DbSet<AssetItem> AssetItems { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.EnableSensitiveDataLogging();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      User fellaBrother = new User("WzBeats", configuration["Env:PerformanceArtist"], true) { Id = 1 };
      User leatherMan = new User("Legys", configuration["Env:DungeonMaster"], true) { Id = 2 };
      modelBuilder.Entity<User>().HasData(new User[] { fellaBrother, leatherMan });



      base.OnModelCreating(modelBuilder);
    }
  }
}
