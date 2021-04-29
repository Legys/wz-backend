using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace WzBeatsApi.Models
{
  public class TrackItem
  {
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Bpm { get; set; }
    public string SongKey { get; set; }
    public string Genre { get; set; }
    public bool IsSold { get; set; }
    public int Listeners { get; set; }
    public int Likes { get; set; }

    public List<AssetItem> Assets { get; set; }
    // public AssetItem TrackAsset { get; set; }
    // public AssetItem CoverAsset { get; set; }

  }
}
