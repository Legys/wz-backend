using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using System.Linq;

namespace WzBeatsApi.Models
{
  public class AssetItem
  {

    public long Id { get; set; }
    public string Title { get; set; }
    public AssetType Type { get; set; }
    public string Url { get; set; }
    public long TrackItemId { get; set; }
    public TrackItem TrackItem { get; set; }

    public static AssetItemResponse MapIndexResponse(AssetItem assetItem)
    {
      var config = new MapperConfiguration(cfg => cfg.CreateMap<AssetItem, AssetItemResponse>());

      var mapper = new Mapper(config);
      return mapper.Map<AssetItemResponse>(assetItem);
    }

    public AssetItem() { }

    public AssetItem(string Title, AssetType Type, string Url)
    {
      this.Title = Title;
      this.Type = Type;
      this.Url = Url;
    }
  }

  public enum AssetType
  {
    Cover,
    Track
  }
}
