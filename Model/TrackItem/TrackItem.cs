using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace WzBeatsApi.Models
{
  public class TrackItem
  {
    public long Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Bpm { get; set; }
    [Required]
    public string SongKey { get; set; }
    [Required]
    public string Genre { get; set; }
    public bool IsSold { get; set; }
    public int Listeners { get; set; }
    public int Likes { get; set; }

    public AssetItem TrackAsset { get; set; }
    public long TrackAssetId { get; set; }
    public AssetItem CoverAsset { get; set; }
    public long CoverAssetId { get; set; }

    public TrackItem() { }

    public TrackItem(string Title, string Description, string Bpm, string SongKey, string Genre, AssetItem TrackAsset, AssetItem CoverAsset)
    {
      this.Title = Title;
      this.Description = Description;
      this.Bpm = Bpm;
      this.SongKey = SongKey;
      this.Genre = Genre;
      this.TrackAsset = TrackAsset;
      this.CoverAsset = CoverAsset;
    }

  }
}
