namespace WzBeatsApi.Models
{
  public class TrackItem
  {
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Bpm { get; set; }
    public bool IsSold { get; set; }
    public int Listeners { get; set; }
    public int Likes { get; set; }
    public string Url { get; set; }
    public string CoverUrl { get; set; }
    private string IsProcessed { get; set; }
  }
}
