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
  }
}
