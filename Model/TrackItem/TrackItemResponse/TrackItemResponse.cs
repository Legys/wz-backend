using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace WzBeatsApi.Models
{
  public class TrackItemResponse
  {
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Bpm { get; set; }
    public string SongKey { get; set; }

    public string Genre { get; set; }

    public AssetItemResponse CoverAsset { get; set; }

    public AssetItemResponse TrackAsset { get; set; }


  }
}
