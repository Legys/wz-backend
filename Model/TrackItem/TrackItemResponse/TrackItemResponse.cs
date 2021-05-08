using System;

namespace WzBeatsApi.Models
{
  public class TrackItemResponse
  {
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int Bpm { get; set; }
    public string SongKey { get; set; }

    public string Genre { get; set; }

    public string Mood { get; set; }
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public AssetItemResponse CoverAsset { get; set; }

    public AssetItemResponse TrackAsset { get; set; }


  }
}
