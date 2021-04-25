namespace WzBeatsApi.Models
{
  public class AssetItem
  {
    public long Id { get; set; }
    public string Title { get; set; }
    public enum Type
    {
      cover,
      track
    }
    public string Url { get; set; }
  }
}
