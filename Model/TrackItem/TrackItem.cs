using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using System.Linq;
using System;

namespace WzBeatsApi.Models
{
  public class TrackItem
  {
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Bpm { get; set; }
    public string SongKey { get; set; }
    public string Genre { get; set; }
    public string Mood { get; set; }
    public bool IsSold { get; set; }
    public int Listeners { get; set; }
    public int Likes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<AssetItem> Assets { get; set; }

    public static TrackItemResponse MapIndexResponse(TrackItem trackItem)
    {
      var config = new MapperConfiguration(cfg =>
      {
        cfg.CreateMap<TrackItem, TrackItemResponse>()
          .ForMember("CoverAsset", opt => opt.MapFrom(ti => ti.Assets.ToList().Find(a => a.Type == AssetType.Cover)))
          .ForMember("TrackAsset", opt => opt.MapFrom(ti => ti.Assets.ToList().Find(a => a.Type == AssetType.Track)));

        cfg.CreateMap<AssetItem, AssetItemResponse>();
      });

      var mapper = config.CreateMapper();
      return mapper.Map<TrackItemResponse>(trackItem);
    }

    public TrackItem() { }

    public TrackItem(string Title, string Description, int Bpm, string SongKey, string Genre, ICollection<AssetItem> Assets)
    {
      this.Title = Title;
      this.Description = Description;
      this.Bpm = Bpm;
      this.SongKey = SongKey;
      this.Genre = Genre;
      this.Assets = Assets;
      this.CreatedAt = DateTime.UtcNow;
    }

    public void Update()
    {
      this.UpdatedAt = DateTime.UtcNow;
    }
  }
}
