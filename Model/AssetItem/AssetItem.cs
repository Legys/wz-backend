using System.ComponentModel.DataAnnotations.Schema;

namespace WzBeatsApi.Models
{
  public class AssetItem
  {
    public long Id { get; set; }
    public string Title { get; set; }
    public AssetType Type { get; set; }
    public string Url { get; set; }

    [ForeignKey("Id")]
    public TrackItem TrackItem { get; set; }

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
